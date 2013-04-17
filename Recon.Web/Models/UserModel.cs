using Recon.Domain.Reference;
using Recon.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recon.Web.Models
{
    public class UserModel
    {
        [Display(Name = "COUNTRY")]
        public Entity Country { get; set; }
        [Display(Name = "ROLE")]
        public String Role { get; set; }
        [Display(Name = "LOGIN")]
        public String Login { get; set; }
        [Display(Name = "EMAIL")]
        public String Email { get; set; }
        [Display(Name = "LAST CONNECTION")]
        public DateTime LastConnection { get; set; }

        public UserModel(UserProfile userProfile)
        {
            this.Email = userProfile.Email;
            this.Country = userProfile.Country;
            this.Login = userProfile.Name;
        }

    }
}