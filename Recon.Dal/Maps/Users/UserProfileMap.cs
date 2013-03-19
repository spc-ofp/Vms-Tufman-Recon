using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recon.Domain.Users;
using FluentNHibernate.Mapping;
using Recon.Domain.Reference;

namespace Recon.Dal.Maps.Users
{
    public class UserProfileMap : ClassMap<UserProfile>
    {
        public UserProfileMap()
        {
            ReadOnly();
            Schema("dbo");
            Table("UserProfile");
            Id(x => x.Id).Column("UserId");
            Map(x => x.Name).Column("UserName");
            Map(x => x.Email).Column("Email");
            References<Entity>(x => x.Country, "Country");
        }
    }
}
