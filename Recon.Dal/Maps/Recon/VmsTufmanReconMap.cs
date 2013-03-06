using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Recon;
using Recon.Domain.Reference;

namespace Recon.Dal.Maps.Recon
{
    public class VmsTufmanReconMap : ClassMap<VmsTufmanRecon>
    {
        public VmsTufmanReconMap()
        {
            ReadOnly();
            Schema("dbo");
            Table("tufman_vms_report");
            Id(x => x.Id).Column("id");
            References<Entity>(x => x.Country, "country");
            Map(x => x.Year).Column("year");
            Map(x => x.VesselId).Column("vessel_id");
            Map(x => x.VesselName).Column("vessel_name");
            References<Gear>(x => x.Gear, "gear");
            Map(x => x.VmsStartdate).Column("vms_start_date");
            Map(x => x.VmsEndDate).Column("vms_end_date");
            Map(x => x.VmsStartPort).Column("vms_start_port");
            Map(x => x.VmsEndPort).Column("vms_end_port");
            Map(x => x.LogsheetStartdate).Column("tufman_depart_date");
            Map(x => x.LogsheetEndDate).Column("tufman_return_date");
            Map(x => x.LogsheetStartPort).Column("tufman_start_port");
            Map(x => x.LogsheetEndPort).Column("tufman_end_port");
            Map(x => x.LogsheetTripId).Column("tufman_trip_id");
            Map(x => x.VmsTripId).Column("vms_trip_id");
            Map(x => x.NationalFleet).Column("national_fleet");
            Map(x => x.VesselFishingCompany).Column("fishing_company");
            References<Entity>(x => x.VesselFlag, "flag");
            Map(x => x.VmsNbDays).Column("vms_nb_days");
            Map(x => x.LogsheetNbDays).Column("tufman_nb_days");
            Map(x => x.TotLogsheetNbDays).Column("tot_tufman_nb_days");
            Map(x => x.TotVmsNbDays).Column("tot_vms_nb_days");
            Map(x => x.EzInOut).Column("ez_in_out");
            Map(x => x.IsFishingTrip).Column("is_fishing_trip");
        }
    }
}
