using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recon.Domain.Recon
{
    public class VmsNotFishingTripRequest
    {
        public virtual int VmsTripId { get; set; }
        public virtual string TufmanCode { get; set; }
        public virtual DateTime RequestDate { get; set; }

        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode = hashCode ^ TufmanCode.GetHashCode() ^ VmsTripId.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as VmsTufmanCoverage;
            if (toCompare == null)
            {
                return false;
            }
            return (this.GetHashCode() != toCompare.GetHashCode());
        }
    }
}
