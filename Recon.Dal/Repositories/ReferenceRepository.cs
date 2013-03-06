using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Recon.Domain.Reference;
using NHibernate.Linq;
using Recon.Domain.Recon;

namespace Recon.Dal.Repositories
{
    public class ReferenceRepository
    {
        protected ISession _session;

        public ReferenceRepository(ISession session)
        {
            _session = session;
        }

        public List<Gear> GetGearFromTufman(String tufman)
        {
           return _session.Query<VmsTufmanRecon>().Where(x => x.Country.Code.Equals(tufman)).Select(x => x.Gear).Distinct().ToList<Gear>();
        }

        public List<int> GetYearFromTufman(String tufman)
        {
            List<int> years = _session.Query<VmsTufmanRecon>().Where(x => x.Country.Code.Equals(tufman)).Select(x => x.Year).Distinct().ToList<int>();
            years.Sort();
            return years;
        }
    }
}
