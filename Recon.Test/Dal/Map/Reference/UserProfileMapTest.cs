using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recon.Web.Tests.TestHelper;
using Recon.Dal.Repositories;
using Recon.Domain.Reference;
using Recon.Domain.Users;

namespace Recon.Test.Dal.Map.Reference
{
    [TestClass]
    public class UserProfileMapTest
    {
        [TestMethod]
        public void Simple_Map_Test()
        {
            UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
            Repository repo = new Repository(unitOfWork.Session);
            UserProfile userProfile = repo.Find<UserProfile>(m => m.Name.Equals("brunod")).FirstOrDefault();
            Assert.AreEqual("brunod@spc.int", userProfile.Email);
            unitOfWork.Rollback();
            unitOfWork.Dispose();
        }
    }
}
