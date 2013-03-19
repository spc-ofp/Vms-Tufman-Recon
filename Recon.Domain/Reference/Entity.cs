using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Recon.Domain.Users;

namespace Recon.Domain.Reference
{
    public class Entity
    {
        [StringLength(2)]
        public virtual String Code { get; set; }
        public virtual String Label { get; set; }
        public virtual IList<UserProfile> Users { get; set; }

        public Entity()
        {
            this.Users = new List<UserProfile>();
        }
    }
}
