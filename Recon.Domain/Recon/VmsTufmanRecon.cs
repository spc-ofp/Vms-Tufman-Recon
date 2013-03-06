using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recon.Domain.Reference;
using System.ComponentModel.DataAnnotations;

namespace Recon.Domain.Recon
{
    public class VmsTufmanRecon
    {
        public virtual int Id { get; set; }
        public virtual Entity Country { get; set; }
        public virtual int VmsTripId { get; set; }
        public virtual int Year { get; set; }
        public virtual int LogsheetTripId { get; set; }
        public virtual int VesselId { get; set; }
        public virtual string VesselName { get; set; }
        public virtual Gear Gear { get; set; }
        public virtual string VesselFishingCompany { get; set; }
        public virtual DateTime? VmsStartdate { get; set; }
        public virtual DateTime? VmsEndDate { get; set; }
        public virtual string VmsStartPort { get; set; }
        public virtual string VmsEndPort { get; set; }
        public virtual DateTime? LogsheetStartdate { get; set; }
        public virtual DateTime? LogsheetEndDate { get; set; }
        public virtual string LogsheetStartPort { get; set; }
        public virtual string LogsheetEndPort { get; set; }
        public virtual double? VmsNbDays { get; set; }
        public virtual double? TotVmsNbDays { get; set; }
        public virtual int? LogsheetNbDays { get; set; }
        public virtual int? TotLogsheetNbDays { get; set; }
        public virtual bool NationalFleet { get; set; }
        public virtual Entity VesselFlag { get; set; }
        [StringLength(3)]
        public virtual string EzInOut { get; set; }
        public virtual bool IsFishingTrip { get; set; }
    }
}
