using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recon.Domain.Users
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual String Label { get; set; }
        public virtual IList<User> Users { get; set; }

        public Role()
        {
            this.Users = new List<User>();
        }
    }
}
