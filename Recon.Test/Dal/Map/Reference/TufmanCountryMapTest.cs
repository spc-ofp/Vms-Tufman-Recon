using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recon.Web.Tests.TestHelper;
using Recon.Dal.Repositories;
using Recon.Domain.Reference;

namespace Recon.Test.Dal.Map.Reference
{
    [TestClass]
    public class TufmanCountryMapTest
    {
        [TestMethod]
        public void Simple_Map_Test()
        {
            UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
            Repository repo = new Repository(unitOfWork.Session);
            List<TufmanCountry> list = repo.GetAll<TufmanCountry>().ToList();
            Assert.AreEqual(16, list.Count);
            unitOfWork.Rollback();
            unitOfWork.Dispose();
        }
    }
}
