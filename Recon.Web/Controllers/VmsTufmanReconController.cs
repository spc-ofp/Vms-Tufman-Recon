using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using NHibernate;
using DoddleReport.Web;
using DoddleReport;
using WebMatrix.WebData;
using System.Web.Security;
using Recon.Web;
using Recon.Web.Models;
using Recon.Domain.Users;
using Recon.Domain.Recon;
using Recon.Domain.Reference;
using Recon.Dal.Repositories;

namespace Spc.Ofp.Recon.Web.Controllers
{
    [Authorize]
    public class VmsTufmanReconController : Controller
    {
        private readonly Repository _repo;
        private readonly ReconRepository _reconRepo;
        private readonly ReferenceRepository _referenceRepo;
        private readonly string _nationalFleetCode = "XX";
        private String _country;
        private String _role;

        public VmsTufmanReconController()
        {
            _repo = new Repository(MvcApplication.UnitOfWork.Session);
            _reconRepo = new ReconRepository(MvcApplication.UnitOfWork.Session);
            _referenceRepo = new ReferenceRepository(MvcApplication.UnitOfWork.Session);
        }

        public ActionResult Error()
        {
            return View();
        }
        
        private void GetUserDetails()
        {
            var context = new UsersContext();
            var username = User.Identity.Name;
            String[] role = Roles.GetRolesForUser(username);
            if (role.Count() == 0)
            {
                WebSecurity.Logout();
                RedirectToAction("Error");
            }
            _role = role[0];
            if (this._role.Equals(RoleList.Country))
            {
                UserProfileModel user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);
                this._country = user.Country;
            }
        }

        public ActionResult Index(string tufman)
        {
            GetUserDetails();
            ViewBag.HidePageGuide = (Request.Browser.Browser.ToUpper() == "IE" && Request.Browser.MajorVersion == 8) ? true :  false;
            if (_role.Equals(RoleList.Admin) || _role.Equals(RoleList.Region))
            {
                var tufmanList = _repo.GetAll<TufmanCountry>().OrderBy(c=>c.Label).Select(c => new SelectListItem { Value = c.Code, Text = c.Label }).ToList();
                ViewBag.tufman = tufmanList;
                if (String.IsNullOrEmpty(tufman))
                {
                    ViewBag.warningMessage = "Select TUFMAN country";
                    ViewBag.showFilters = false;
                    return View();
                }
            }
            else
                tufman = _country;

            ViewBag.showFilters = true;
            ViewBag.year = new SelectList(_referenceRepo.GetYearFromTufman(tufman));
            ViewBag.gear = _referenceRepo.GetGearFromTufman(tufman).OrderBy(x=>x.Label).Select(x=> new SelectListItem {Value=x.Code, Text = x.Label}).ToList();
            ViewBag.criteria = new SelectList(CriteriaList.GetCriterias());

            List<int> nbDaysLst = new List<int>();
            for (int i = 0; i < 6; i++)
                nbDaysLst.Add(i);
            ViewBag.minNbDays = new SelectList(nbDaysLst);

            //2. Tufman restore status
            int lastRestore = (DateTime.Now - _repo.Get<TufmanLastRestore>(tufman).RestoreDate).Days;
            string message = "TUFMAN last entry received at SPC " + lastRestore + " day(s) ago !";
            if (lastRestore <= 7)
                ViewBag.successMessage = message;
            else
                if (lastRestore <= 30)
                    ViewBag.warningMessage = message;
                else
                    ViewBag.errorMessage = message;
            ViewBag.selectedTufman = tufman;
            return View();
        }

        [HttpGet]
        public PartialViewResult Search(string button, string tufman, string gear, string vesselName, string year, string fleet, string criteria, string fishingCompany, int minNbDays)
        {
            GetUserDetails();
            if (_role == RoleList.Country && !tufman.Equals(_country))
            {
                WebSecurity.Logout();
                return PartialView("_Error");
            }
            fleet = (String.IsNullOrEmpty(fleet) || fleet.Equals("0")) ? null : fleet;
            List<VmsTufmanReconModel> modelLst = new List<VmsTufmanReconModel>();
            List<VmsTufmanRecon> reconLst = new List<VmsTufmanRecon>();
            //user clicked on detail, return the list of recon
            ViewBag.GlobalTufman = tufman;
            ViewBag.GlobalButton = button;
            ViewBag.GlobalGear = gear;
            ViewBag.GlobalVesselName = vesselName;
            ViewBag.GlobalYear = year;
            ViewBag.GlobalFleet = fleet;
            ViewBag.GlobalCriteria = criteria;
            ViewBag.GlobalFishingCompany = fishingCompany;
            ViewBag.GlobalMinNbDays = minNbDays;

            if (button.Equals("Detail"))
            {
                fishingCompany = (String.IsNullOrEmpty(fishingCompany) || fishingCompany.Equals("0")) ? null : fishingCompany;
                reconLst = _reconRepo.FilterRecon(tufman, gear, vesselName, year, fleet, criteria, fishingCompany);
                if (fleet != null && fleet.Equals(_nationalFleetCode))
                {
                    foreach (VmsTufmanRecon recon in reconLst)
                        modelLst.Add(new VmsTufmanReconModel(recon, true));
                }
                else
                {
                    foreach (VmsTufmanRecon recon in reconLst)
                        modelLst.Add(new VmsTufmanReconModel(recon, false));
                }
                //Remove any Recon where the logsheet or the vms duration is less than the minNbDays parameter
                if (minNbDays > 0)
                    modelLst.RemoveAll(x => ((x.VmsTripId != 0 && x.VmsNbDays < minNbDays) || (x.LogsheetTripId != 0 && x.LogsheetNbDays < minNbDays)));
                
                return PartialView("_VmsTufmanReconDetail", modelLst);
            }
            //user clicked on coverage rate
            List<VmsTufmanCoverage> modelList = _reconRepo.FilterCoverage(tufman, gear, year, fleet);
            if(String.IsNullOrEmpty(year))
                ViewBag.ReconChart = ReconLineChart(modelList);
            else
                ViewBag.ReconChart = ReconBarChart(modelList);
            return PartialView("_Coverage", modelList);
        }

        private object[][] ReconLineChart(List<VmsTufmanCoverage> vmsTufmanCoverages)
        {
            List<int> yearLst = vmsTufmanCoverages.Select(x => x.Year).Distinct().ToList<int>();
            yearLst.Sort();
            List<Entity> fleetLst = vmsTufmanCoverages.Select(x => x.Fleet).Distinct().ToList<Entity>();
            object[][] coverageChart = new object[yearLst.Count + 1][];
            object[] headerRow = new object[fleetLst.Count + 1];
            headerRow[0] = "Years";

            for (int i = 0; i < fleetLst.Count; i++)
            {
                headerRow[i + 1] = fleetLst[i].Label;
            }
            coverageChart[0] = headerRow;

            for (int j = 0; j < yearLst.Count; j++)
            {
                object[] row = new object[fleetLst.Count + 1];
                row[0] = yearLst[j].ToString();
                for (int i = 0; i < fleetLst.Count; i++)
                {
                    VmsTufmanCoverage temp = vmsTufmanCoverages.Where(x => x.Year.Equals(yearLst[j]) && x.Fleet.Code.Equals(fleetLst[i].Code)).FirstOrDefault();
                    if (temp != null)
                        row[i + 1] = temp.GetCoverage();
                }
                coverageChart[j + 1] = row;
            }
            return coverageChart;
        }

        private object[][] ReconBarChart(List<VmsTufmanCoverage> vmsTufmanCoverages)
        {
            List<int> yearLst = vmsTufmanCoverages.Select(x => x.Year).Distinct().ToList<int>();
            yearLst.Sort();
            List<Entity> fleetLst = vmsTufmanCoverages.Select(x => x.Fleet).Distinct().ToList<Entity>();
            object[][] coverageChart = new object[yearLst.Count + 1][];
            object[] headerRow = new object[fleetLst.Count + 1];
            headerRow[0] = "Years";

            for (int i = 0; i < fleetLst.Count; i++)
            {
                headerRow[i + 1] = fleetLst[i].Label;
            }
            coverageChart[0] = headerRow;

            for (int j = 0; j < yearLst.Count; j++)
            {
                object[] row = new object[fleetLst.Count + 1];
                row[0] = yearLst[j].ToString();
                for (int i = 0; i < fleetLst.Count; i++)
                {
                    VmsTufmanCoverage temp = vmsTufmanCoverages.Where(x => x.Year.Equals(yearLst[j]) && x.Fleet.Code.Equals(fleetLst[i].Code)).FirstOrDefault();
                    if (temp != null)
                        row[i + 1] = temp.GetCoverage();
                }
                coverageChart[j + 1] = row;
            }
            return coverageChart;
        }

        public ActionResult Fleets(string tufman, string gear)
        {
            GetUserDetails();
            if (_role == RoleList.Country && !tufman.Equals(_country))
            {
                WebSecurity.Logout();
                return PartialView("_Error");
            }
            List<Entity> fleetLst = _reconRepo.GetFleets(tufman, gear, true);
            return Json(fleetLst.Select(x => new { Value = x.Code, Text = x.Label }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FishingCompanies(string tufman, string gear, string fleet)
        {
            GetUserDetails();
            if (_role == RoleList.Country && !tufman.Equals(_country))
            {
                WebSecurity.Logout();
                return PartialView("_Error");
            }
            var k = _reconRepo.FilterRecon(tufman, gear, null, null, fleet, null, null).Select(x => new { Value = x.VesselFishingCompany, Text = x.VesselFishingCompany }).OrderBy(x => x.Text);
            return Json(k.Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ReportResult ExportIndex(string tufman, string gear, string vesselName, string year, string fleet, string criteria, string fishingCompany, int? minNbDays)
        {
            GetUserDetails();
            if (_role == RoleList.Country && !tufman.Equals(_country))
            {
                WebSecurity.Logout();
                return null;
            }
            fleet = (String.IsNullOrEmpty(fleet) || fleet.Equals("0")) ? null : fleet;
            fishingCompany = (String.IsNullOrEmpty(fishingCompany) || fishingCompany.Equals("0")) ? null : fishingCompany;

            List<VmsTufmanReconModel> modelLst = new List<VmsTufmanReconModel>();
            List<VmsTufmanRecon> reconLst = _reconRepo.FilterRecon(tufman, gear, vesselName, year, fleet, criteria, fishingCompany);
            string eez;
            if (fleet != null && fleet.Equals(_nationalFleetCode))
            {
                foreach (VmsTufmanRecon recon in reconLst)
                    modelLst.Add(new VmsTufmanReconModel(recon, true));
                eez = "WCPFC Convention Area";
            }
            else
            {
                foreach (VmsTufmanRecon recon in reconLst)
                    modelLst.Add(new VmsTufmanReconModel(recon, false));
                eez = _repo.Get<Entity>(tufman).Label;
            }
            //Remove any Recon where the logsheet or the vms duration is less than the minNbDays parameter
            if (minNbDays > 0)
                modelLst.RemoveAll(x => ((x.VmsTripId != 0 && x.VmsNbDays < minNbDays) || (x.LogsheetTripId != 0 && x.LogsheetNbDays < minNbDays)));

            var report = new Report(modelLst.ToReportSource());
            report.TextFields.Title = "VMS - Logsheet Reconciliation Detail";
            report.DataFields["VmsTripId"].Hidden = true;
            report.DataFields["VesselId"].Hidden = true;
            report.DataFields["LogsheetTripId"].Hidden = true;
            report.DataFields["Country"].Hidden = true;
            report.DataFields["VesselFlag"].FormatAs<Entity>(x => x.Label);
            report.DataFields["Gear"].FormatAs<Gear>(x => x.Label);

            report.RenderHints.Orientation = ReportOrientation.Landscape;

            year = (String.IsNullOrEmpty(year) ? "ALL" : year);
            fishingCompany = (String.IsNullOrEmpty(fishingCompany) || fishingCompany.Equals("0")) ? "ALL" : fishingCompany;

            //var gearRepo = new Repository<Gear>(MvcApplication.UnitOfWork.Session);
            string gearDesc = _repo.Get<Gear>(gear).Label;
            string fleetDesc = (String.IsNullOrEmpty(fleet)) ? "ALL" : _repo.Get<Entity>(fleet).Label;

            report.TextFields.Header = string.Format(@"
            Gear:  {0} 
            Fleet:  {1} 
            Year:  {2} 
            EEZ:  {3}
            Fishing Company:  {4}
            Criteria:  {5}
            Min Trip Duration:  {6}", gearDesc, fleetDesc, year, eez, fishingCompany, criteria, minNbDays);

            return new ReportResult(report);
        }

        public ReportResult ExportCoverage(string tufman, string gear, string year, string fleet)
        {
            GetUserDetails();
            if (_role == RoleList.Country && !tufman.Equals(_country))
            {
                WebSecurity.Logout();
                return null;
            }
            List<VmsTufmanCoverage> modelList = _reconRepo.FilterCoverage(tufman, gear, year, fleet);
            var report = new Report(modelList.ToReportSource());
            report.TextFields.Title = "VMS - Logsheet Reconciliation Coverage";
            report.DataFields["Country"].Hidden = true;
            report.DataFields["Fleet"].FormatAs<Entity>(x => x.Label);
            report.DataFields["Gear"].FormatAs<Gear>(x => x.Label);
            report.RenderHints.Orientation = ReportOrientation.Landscape;
            year = (String.IsNullOrEmpty(year) ? "ALL" : year);
            return new ReportResult(report);
        }

        [HttpPost]
        public ActionResult SetAsNonFishingTrip(int vmsTripId, string tufmanCode)
        {
            GetUserDetails();
            if (_role == RoleList.Country && !tufmanCode.Equals(_country))
            {
                WebSecurity.Logout();
                return PartialView("_Error");
            }
            if (_repo.Find<VmsNotFishingTripRequest>(x => x.VmsTripId == vmsTripId && x.TufmanCode.Equals(tufmanCode)).Count() > 0)
            {
                 return Json(new {Status = "Request for this non-fishing trip already sent to SPC and under investigation"});
            }
            VmsNotFishingTripRequest tripRequest = new VmsNotFishingTripRequest();
            tripRequest.VmsTripId = vmsTripId;
            tripRequest.TufmanCode = tufmanCode;
            tripRequest.RequestDate = DateTime.Now;
            _repo.Save<VmsNotFishingTripRequest>(tripRequest);
            MvcApplication.UnitOfWork.Commit();
            return Json(new { Status = "Request for non-fishing trip sent to SPC for investigation" });
        }
    }
}
