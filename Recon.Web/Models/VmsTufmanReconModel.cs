using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Recon.Domain.Reference;
using System.ComponentModel.DataAnnotations;
using Recon.Domain.Recon;

namespace Recon.Web.Models
{
    public class VmsTufmanReconModel
    {
        public virtual Entity Country { get; set; }
        [Display(Name = "VMS TRIP ID")]
        public virtual int VmsTripId { get; set; }
        [Display(Name = "YEAR")]
        public virtual int Year { get; set; }
        [Display(Name = "FISHING COMPANY")]
        public virtual string VesselFishingCompany { get; set; }
        [Display(Name = "LOGSHEET TRIP ID")]
        public virtual int LogsheetTripId { get; set; }
        [Display(Name = "VESSEL ID")]
        public virtual int VesselId { get; set; }
        [Display(Name = "VESSEL NAME")]
        public virtual string VesselName { get; set; }
        [Display(Name = "FLAG")]
        public virtual Entity VesselFlag { get; set; }
        [Display(Name = "GEAR")]
        public virtual Gear Gear { get; set; }
        [Display(Name = "VMS START DATE")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public virtual DateTime? VmsStartdate { get; set; }
        [Display(Name = "VMS END DATE")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ConvertEmptyStringToNull = true)]
        public virtual DateTime? VmsEndDate { get; set; }
        [Display(Name = "VMS START PORT")]
        public virtual string VmsStartPort { get; set; }
        [Display(Name = "VMS END PORT")]
        public virtual string VmsEndPort { get; set; }
        [Display(Name = "LOG START DATE")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ConvertEmptyStringToNull = true)]
        public virtual DateTime? LogsheetStartdate { get; set; }
        [Display(Name = "LOG END DATE")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ConvertEmptyStringToNull = true)]
        public virtual DateTime? LogsheetEndDate { get; set; }
        [Display(Name = "LOG START PORT")]
        public virtual string LogsheetStartPort { get; set; }
        [Display(Name = "LOG END PORT")]
        public virtual string LogsheetEndPort { get; set; }
        [Display(Name = "VMS NB DAYS")]
        public virtual double? VmsNbDays { get; set; }
        [Display(Name = "LOG NB DAYS")]
        public virtual int? LogsheetNbDays { get; set; }
        [Display(Name = "IS FISHINF TRIP?")]
        public virtual bool IsFishingTrip { get; set; }


        public VmsTufmanReconModel(VmsTufmanRecon recon, bool isNationalFleet)
        {
            this.VmsTripId = recon.VmsTripId;
            this.LogsheetTripId = recon.LogsheetTripId;
            this.Country = recon.Country;
            this.Year = recon.Year;
            this.VesselId = recon.VesselId;
            this.VesselName = recon.VesselName;
            this.Gear = recon.Gear;
            this.VesselFishingCompany = recon.VesselFishingCompany;
            this.VmsStartdate = recon.VmsStartdate;
            this.VmsEndDate = recon.VmsEndDate;
            this.VmsStartPort = recon.VmsStartPort;
            this.VmsEndPort = recon.VmsEndPort;
            this.LogsheetStartdate = recon.LogsheetStartdate;
            this.LogsheetEndDate = recon.LogsheetEndDate;
            this.LogsheetStartPort = recon.LogsheetStartPort;
            this.LogsheetEndPort = recon.LogsheetEndPort;
            if (isNationalFleet)
            {
                this.VmsNbDays = recon.TotVmsNbDays;
                this.LogsheetNbDays = recon.TotLogsheetNbDays;
            }
            else
            {
                this.VmsNbDays = recon.VmsNbDays;
                this.LogsheetNbDays = recon.LogsheetNbDays;
            }
            this.VesselFlag = recon.VesselFlag;
            this.IsFishingTrip = recon.IsFishingTrip;
        }
    }
}