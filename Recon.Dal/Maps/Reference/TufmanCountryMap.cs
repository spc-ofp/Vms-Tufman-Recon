using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Reference;

namespace Recon.Dal.Maps.Reference
{
    class TufmanCountryMap : ClassMap<TufmanCountry>
    {
        public TufmanCountryMap()
        {
            ReadOnly();
            Schema("ref");
            Table("tufman_countries");
            Id(x => x.Code).Column("entity_code");
            Map(x => x.Label).Column("entity_short");
        }
    }
}
