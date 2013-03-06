using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Users;
using Recon.Domain.Reference;

namespace Recon.Dal.Maps.Users
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Schema("dbo");
            Table("UserProfile");
            Id(x => x.Id).Column("UserId");
            Map(x => x.Name).Column("UserName");
            Map(x => x.Email).Column("Email");
            References<Entity>(x => x.Country, "Country");
        }
    }
}
