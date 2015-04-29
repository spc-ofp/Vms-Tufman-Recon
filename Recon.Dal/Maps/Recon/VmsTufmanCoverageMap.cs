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
            Map(x => x.BumC).Column("bum_c");
            Map(x => x.BumR).Column("bum_r");
            Map(x => x.BlmC).Column("blm_c");
            Map(x => x.BlmR).Column("blm_r");
            Map(x => x.MlsC).Column("mls_c");
            Map(x => x.MlsR).Column("mls_r");
            Map(x => x.SwoC).Column("swo_c");
            Map(x => x.SwoR).Column("swo_r");
            Map(x => x.SfaC).Column("sfa_c");
            Map(x => x.SfaR).Column("sfa_r");
            Map(x => x.FalC).Column("fal_c");
            Map(x => x.FalR).Column("fal_r");
            Map(x => x.BshC).Column("bsh_c");
            Map(x => x.BshR).Column("bsh_r");
            Map(x => x.OcsC).Column("ocs_c");
            Map(x => x.OcsR).Column("ocs_r");
            Map(x => x.ThrC).Column("thr_c");
            Map(x => x.ThrR).Column("thr_r");
            Map(x => x.MakC).Column("mak_c");
            Map(x => x.MakR).Column("mak_r");
            Map(x => x.HamC).Column("ham_c");
            Map(x => x.HamR).Column("ham_r");
            Map(x => x.PorC).Column("por_c");
            Map(x => x.PorR).Column("por_r");
        }
    }
}
