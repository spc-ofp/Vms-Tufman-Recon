using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Recon;

namespace Recon.Dal.Maps.Recon
{
    public class VmsNotFishingTripRequestMap : ClassMap<VmsNotFishingTripRequest>
    {
        public VmsNotFishingTripRequestMap()
        {
            ReadOnly();
            Schema("dbo");
            Table("vms_not_fishing_trip_request");
            CompositeId().KeyProperty(x => x.TufmanCode, "tufman_code").KeyProperty(x => x.VmsTripId, "vms_trip_id");
            Map(x => x.RequestDate).Column("request_date");
        }
    }
}
