using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recon.Web.Tests.TestHelper;
using Recon.Domain.Reference;
using Recon.Dal.Repositories;

namespace Recon.Web.Tests.Dal.Map.Reference
{
    [TestClass]
    public class GearMapTest
    {
        [TestMethod]
        public void Simple_Map_Test()
        {
            UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
            Repository repo = new Repository(unitOfWork.Session);
            List<Gear> list = repo.GetAll<Gear>().ToList();
            Assert.AreEqual(2, list.Count);
            unitOfWork.Rollback();
            unitOfWork.Dispose();
        }
    }
}
