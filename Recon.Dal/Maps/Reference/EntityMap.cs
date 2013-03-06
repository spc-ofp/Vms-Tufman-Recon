using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recon.Domain.Reference;
using FluentNHibernate.Mapping;

namespace Recon.Dal.Maps.Reference
{
    public class EntityMap : ClassMap<Entity>
    {
        public EntityMap()
        {
            ReadOnly();
            Schema("ref");
            Table("entities");
            Id(x => x.Code).Column("entity_code");
            Map(x => x.Label).Column("entity_short");
        }
    }
}
