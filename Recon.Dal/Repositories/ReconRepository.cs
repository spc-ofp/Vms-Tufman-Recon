using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Recon.Domain.Recon;
using Recon.Domain.Reference;

namespace Recon.Dal.Repositories
{
    public class ReconRepository
    {
        protected ISession _session;
        private readonly string _nationalFleetCode = "XX";

        public ReconRepository(ISession session)
        {
            _session = session;
        }

        public List<VmsTufmanRecon> FilterRecon(string tufman, string gear, string vesselName, string year, string fleet, string criteria, string fishingCompany)
        {
            var vmsTufmanRecons = _session.Query<VmsTufmanRecon>();

            if (!string.IsNullOrEmpty(vesselName))
                vmsTufmanRecons = vmsTufmanRecons.Where(x => x.VesselName.Contains(vesselName));

            if (!string.IsNullOrEmpty(tufman))
                vmsTufmanRecons = vmsTufmanRecons.Where(x => x.Country.Code.Equals(tufman));

            if (!string.IsNullOrEmpty(year))
                vmsTufmanRecons = vmsTufmanRecons.Where(x => x.Year.Equals(Int32.Parse(year)));

            if (!string.IsNullOrEmpty(gear))
                vmsTufmanRecons = vmsTufmanRecons.Where(x => x.Gear.Code.Equals(gear));

            if (!string.IsNullOrEmpty(fleet))
                vmsTufmanRecons = (fleet.Equals(_nationalFleetCode)) ? vmsTufmanRecons.Where(x => x.NationalFleet == true) : vmsTufmanRecons.Where(x => (x.VesselFlag.Code.Equals(fleet) && x.EzInOut.Equals("IN")));
            else
                vmsTufmanRecons = vmsTufmanRecons.Where(x => x.EzInOut.Equals("IN"));

            if (!string.IsNullOrEmpty(fishingCompany) && !fishingCompany.Equals("0"))
                vmsTufmanRecons = vmsTufmanRecons.Where(x => x.VesselFishingCompany.Equals(fishingCompany));

            if (!string.IsNullOrEmpty(criteria))
                switch (criteria)
                {
                    case CriteriaList.MissingLogsheet:
                        vmsTufmanRecons = vmsTufmanRecons.Where(x => x.LogsheetTripId == null);
                        break;
                    case CriteriaList.MissingVms:
                        vmsTufmanRecons = vmsTufmanRecons.Where(x => x.VmsTripId == null);
                        break;
                    case CriteriaList.Reconciled:
                        vmsTufmanRecons = vmsTufmanRecons.Where(x => x.LogsheetTripId != null).Where(x => x.VmsTripId != null);
                        break;
                }

            return vmsTufmanRecons.OrderBy(x => x.VesselName).ThenBy(x => x.VmsStartdate).ThenBy(x => x.LogsheetStartdate).ToList();
        }

        public List<VmsTufmanCoverage> FilterCoverage(string tufman, string gear, string year, string fleet)
        {
            var vmsTufmanCoverages = _session.Query<VmsTufmanCoverage>();

            if (!string.IsNullOrEmpty(tufman))
                vmsTufmanCoverages = vmsTufmanCoverages.Where(x => x.Country.Code.Equals(tufman));

            if (!string.IsNullOrEmpty(year))
                vmsTufmanCoverages = vmsTufmanCoverages.Where(x => x.Year.Equals(Int32.Parse(year)));

            if (!string.IsNullOrEmpty(gear))
                vmsTufmanCoverages = vmsTufmanCoverages.Where(x => x.Gear.Code.Equals(gear));

            if (!string.IsNullOrEmpty(fleet))
                vmsTufmanCoverages = vmsTufmanCoverages.Where(x => x.Fleet.Code.Equals(fleet));

            return vmsTufmanCoverages.OrderBy(x => x.Fleet.Label).ThenBy(x => x.Year).ToList();
        }

        public List<Entity> GetFleets(string tufman, string gear, Boolean checkNationalFleet)
        {
            var vmsTufmanQuery = _session.Query<VmsTufmanRecon>();
            List<Entity> fleets = new List<Entity>();

            if (!string.IsNullOrEmpty(tufman))
                vmsTufmanQuery = vmsTufmanQuery.Where(x => x.Country.Code.Equals(tufman));

            if (!string.IsNullOrEmpty(gear))
                vmsTufmanQuery = vmsTufmanQuery.Where(x => x.Gear.Code.Equals(gear));

            fleets = vmsTufmanQuery.Select(x => x.VesselFlag).Distinct().ToList();
            fleets = fleets.OrderBy(x => x.Label).ToList();

            if(!checkNationalFleet)
                return fleets;

            List<VmsTufmanRecon> vmsTufmanRecons = vmsTufmanQuery.ToList();
            
            if(vmsTufmanRecons.Select(x => x.NationalFleet).Contains(true))
            {
                Entity nationalFleet = _session.Query<Entity>().Where(x=>x.Code.Equals(_nationalFleetCode)).FirstOrDefault();
                fleets.Insert(0, nationalFleet);
            }
            
            return fleets;
        }
    }
}
