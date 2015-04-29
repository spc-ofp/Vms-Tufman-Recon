using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Recon.Domain.Reference;

namespace Recon.Domain.Recon
{
    public class VmsTufmanCoverage
    {
        public virtual Entity Country { get; set; }
        [Display(Name = "FLAG")]
        public virtual Entity Fleet { get; set; }
        [Display(Name = "YEAR")]
        public virtual int Year { get; set; }
        [Display(Name = "GEAR")]
        public virtual Gear Gear { get; set; }
        [Display(Name = "NB OF TRIPS")]
        public virtual int NbTrips { get; set; }
        [Display(Name = "NB OF VMS TRIPS")]
        public virtual int NbVmsTrips { get; set; }
        [Display(Name = "NB OF LOGSHEET")]
        public virtual int? NbLogsheet { get; set; }
        [Display(Name = "VMS TRIP COVERAGE")]
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public virtual double? VmsTripCov { get; set; }
        [Display(Name = "LOGSHEET TRIP COVERAGE")]
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public virtual double? LogsheetTripCov { get; set; }
        [Display(Name = "RAISED NB OF DAYS")]
        public virtual double? RaisedVmsDays { get; set; }
        [Display(Name = "NB OF LOGSHEET DAYS")]
        public virtual int? NbLogsheetdays { get; set; }
        [Display(Name = "LOGSHEET DAYS COVERAGE")]
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public virtual double? LogsheetDaysCov { get; set; }
        [Display(Name = "ALB")]
        public virtual double? AlbC { get; set; }
        [Display(Name = "ALB RAISED")]
        public virtual double? AlbR { get; set; }
        [Display(Name = "YFT")]
        public virtual double? YftC { get; set; }
        [Display(Name = "YFT RAISED")]
        public virtual double? YftR { get; set; }
        [Display(Name = "BET")]
        public virtual double? BetC { get; set; }
        [Display(Name = "BET RAISED")]
        public virtual double? BetR { get; set; }
        [Display(Name = "SKJ")]
        public virtual double? SkjC { get; set; }
        [Display(Name = "SKJ RAISED")]
        public virtual double? SkjR { get; set; }
        [Display(Name = "BUM")]
        public virtual double? BumC { get; set; }
        [Display(Name = "BUM RAISED")]
        public virtual double? BumR { get; set; }
        [Display(Name = "BLM")]
        public virtual double? BlmC { get; set; }
        [Display(Name = "BLM RAISED")]
        public virtual double? BlmR { get; set; }
        [Display(Name = "MLS")]
        public virtual double? MlsC { get; set; }
        [Display(Name = "MLS RAISED")]
        public virtual double? MlsR { get; set; }
        [Display(Name = "SWO")]
        public virtual double? SwoC { get; set; }
        [Display(Name = "SWO RAISED")]
        public virtual double? SwoR { get; set; }
        [Display(Name = "SFA")]
        public virtual double? SfaC { get; set; }
        [Display(Name = "SFA RAISED")]
        public virtual double? SfaR { get; set; }
        [Display(Name = "FAL")]
        public virtual double? FalC { get; set; }
        [Display(Name = "FAL RAISED")]
        public virtual double? FalR { get; set; }
        [Display(Name = "BSH")]
        public virtual double? BshC { get; set; }
        [Display(Name = "BSH RAISED")]
        public virtual double? BshR { get; set; }
        [Display(Name = "OCS")]
        public virtual double? OcsC { get; set; }
        [Display(Name = "OCS RAISED")]
        public virtual double? OcsR { get; set; }
        [Display(Name = "THR")]
        public virtual double? ThrC { get; set; }
        [Display(Name = "THR RAISED")]
        public virtual double? ThrR { get; set; }
        [Display(Name = "MAK")]
        public virtual double? MakC { get; set; }
        [Display(Name = "MAK RAISED")]
        public virtual double? MakR { get; set; }
        [Display(Name = "HAM")]
        public virtual double? HamC { get; set; }
        [Display(Name = "HAM RAISED")]
        public virtual double? HamR { get; set; }
        [Display(Name = "POR")]
        public virtual double? PorC { get; set; }
        [Display(Name = "POR RAISED")]
        public virtual double? PorR { get; set; }

        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode = hashCode ^ Country.GetHashCode() ^ Gear.GetHashCode() ^ Fleet.GetHashCode() ^ Year.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as VmsTufmanCoverage;
            if (toCompare == null)
            {
                return false;
            }
            return (this.GetHashCode() != toCompare.GetHashCode());
        }

        public virtual double? GetCoverage()
        {
            if (LogsheetTripCov > LogsheetDaysCov)
                return LogsheetTripCov;
            if (LogsheetDaysCov == null)
                return Double.NaN;
            return (LogsheetDaysCov);
        }
    }
}
