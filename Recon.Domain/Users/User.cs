﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recon.Domain.Reference;

namespace Recon.Domain.Users
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual Entity Country { get; set; }
        public virtual String Email { get; set; }
        public virtual IList<Role> Roles { get; set; }

        public User()
        {
            this.Roles = new List<Role>();
        }
    }

}
