using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Recon.Domain.Reference;
using Recon.Domain.Recon;
using System.Text.RegularExpressions;

namespace Recon.Web.Models
{
    public class VmsTufmanCoverageModel
    {
        public string id { get; set; }
        public string tufman { get; set; }
        public string year { get; set; }
        public string gear { get; set; }
        public Entity fleet{ get; set; }
        public string fishingCompany { get; set; } 
        public int nbTrips { get; set; }
        public int vmsNbTrips { get; set; }
        public int logsheetNbTrips { get; set; }
        public double vmsTripCoverage { get; set; }
        public double logsheetTripCoverage { get; set; }
        public double nbDays { get; set; }
        public int logsheetNbDays { get; set; }
        public double logsheetDaysCoverage { get; set; }
        public List<VmsTufmanRecon> reconLst { get; set; }
        
        public double GetCoverage(){
            if (logsheetTripCoverage > logsheetDaysCoverage)
                return logsheetTripCoverage;
            return logsheetDaysCoverage;
        }

        public VmsTufmanCoverageModel(string tufman, string year, string gear, Entity fleet)
        {
            this.tufman = tufman;
            this.year = year;
            this.gear = gear;
            this.fleet = fleet;
            this.id = fleet.Code;
            this.fishingCompany = "ALL";

        }

        public VmsTufmanCoverageModel(string tufman, string year, string gear, Entity fleet, string fishingCompany)
        {
            this.tufman = tufman;
            this.year = year;
            this.gear = gear;
            this.fleet = fleet;
            this.fishingCompany = fishingCompany;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            this.id = rgx.Replace(fleet.Code + "-" + fishingCompany, "");
        }

        public void GenerateCoverage()
        {
            Dictionary<int, double> vmsTrips = new Dictionary<int, double>();
            Dictionary<int, int> logsheetTrips = new Dictionary<int, int>();
            foreach (VmsTufmanRecon recon in this.reconLst)
            {
                if (recon.VmsTripId != 0 && !vmsTrips.ContainsKey(recon.VmsTripId))
                {
                    vmsTrips.Add(recon.VmsTripId, recon.VmsNbDays.Value);

                }
                if (recon.LogsheetTripId != 0 && !logsheetTrips.ContainsKey(recon.LogsheetTripId))
                    logsheetTrips.Add(recon.LogsheetTripId, recon.LogsheetNbDays.Value);
            }
            this.nbTrips = reconLst.Count;
            this.vmsNbTrips = vmsTrips.Count;
            this.logsheetNbTrips = logsheetTrips.Count;
            this.nbDays = vmsTrips.Sum(x => x.Value);
            this.logsheetNbDays = logsheetTrips.Sum(x => x.Value);
            this.vmsTripCoverage = this.vmsNbTrips * 100.0 / this.nbTrips;
            this.vmsTripCoverage = (this.vmsTripCoverage > 100) ? 100 : Math.Round(this.vmsTripCoverage,2);
            this.logsheetTripCoverage = this.logsheetNbTrips * 100.0 / this.nbTrips;
            this.logsheetTripCoverage = (this.logsheetTripCoverage > 100) ? 100 : Math.Round(this.logsheetTripCoverage,2);
            this.nbDays = Math.Round(this.nbDays * 100 / this.vmsTripCoverage,2);
            this.logsheetDaysCoverage = Math.Round(this.logsheetNbDays * 100 / this.nbDays,2);
            this.logsheetDaysCoverage = (this.logsheetDaysCoverage > 100) ? 100 : this.logsheetDaysCoverage;
        }
    }
}