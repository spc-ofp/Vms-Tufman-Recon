using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Recon;
using Recon.Domain.Reference;

namespace Recon.Dal.Maps.Recon
{
    public class VmsTufmanCoverageMap : ClassMap<VmsTufmanCoverage>
    {
        public VmsTufmanCoverageMap()
        {
            ReadOnly();
            Schema("dbo");
            Table("tufman_vms_coverage");
            CompositeId().KeyReference(x => x.Country, "country").KeyReference(x => x.Fleet, "flag").KeyReference(x => x.Gear, "gear").KeyProperty(x => x.Year, "year");
            References<Entity>(x => x.Country, "country");
            References<Gear>(x => x.Gear, "gear");
            References<Entity>(x => x.Fleet, "flag");
            Map(x => x.NbTrips).Column("nb_trips");
            Map(x => x.NbVmsTrips).Column("nb_vms_trips");
            Map(x => x.VmsTripCov).Column("vms_trip_cov");
            Map(x => x.RaisedVmsDays).Column("raised_vms_days");
            Map(x => x.NbLogsheet).Column("nb_logsheet");
            Map(x => x.LogsheetTripCov).Column("logsheet_trip_cov");
            Map(x => x.NbLogsheetdays).Column("nb_logsheet_days");
            Map(x => x.LogsheetDaysCov).Column("logsheet_days_cov");
            Map(x => x.AlbC).Column("alb_c");
            Map(x => x.AlbR).Column("alb_r");
            Map(x => x.YftC).Column("yft_c");
            Map(x => x.YftR).Column("yft_r");
            Map(x => x.BetC).Column("bet_c");
            Map(x => x.BetR).Column("bet_r");
            Map(x => x.SkjC).Column("skj_c");
            Map(x => x.SkjR).Column("skj_r");
        }
    }
}
