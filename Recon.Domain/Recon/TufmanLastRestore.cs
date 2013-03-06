using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Recon.Domain.Recon
{
    public class TufmanLastRestore
    {
        [StringLength(2)]
        public virtual String Code { get; set; }
        public virtual DateTime RestoreDate { get; set; }
    }
}
