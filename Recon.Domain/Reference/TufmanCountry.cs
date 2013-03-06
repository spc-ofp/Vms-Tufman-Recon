using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Recon.Domain.Reference
{
    public class TufmanCountry
    {
        [StringLength(2)]
        public virtual String Code { get; set; }
        public virtual String Label { get; set; }
    }
}
