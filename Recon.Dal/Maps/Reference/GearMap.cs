using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Reference;

namespace Recon.Dal.Maps.Reference
{
    public class GearMap : ClassMap<Gear>
    {
        public GearMap()
        {
            Schema("ref");
            Table("gears");
            Id(x => x.Code).Column("gear_code_2");
            Map(x => x.Label).Column("gear_desc");
        }
    }
}
