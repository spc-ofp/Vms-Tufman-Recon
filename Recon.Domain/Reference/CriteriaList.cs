using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recon.Domain.Reference
{
    public static class CriteriaList
    {
        public const String MissingLogsheet = "Missing Logsheet Only";
        public const String MissingVms = "Missing VMS Only";
        public const String Reconciled = "Reconciled Trips Only";

        public static List<String> GetCriterias(){
            List<String> result = new List<String>();
            result.Add(MissingLogsheet);
            result.Add(MissingVms);
            result.Add(Reconciled);
            return result;
        }
    }
}
