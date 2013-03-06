using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Users;

namespace Recon.Dal.Maps.Users
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Schema("dbo");
            Table("webpages_Roles");
            Id(x => x.Id).Column("RoleId");
            Map(x => x.Label).Column("RoleName");
            HasManyToMany(x => x.Users).Table("webpages_UsersInRoles").ParentKeyColumn("RoleId").ChildKeyColumn("UserId").Inverse().AsBag();
        }
    }
}
