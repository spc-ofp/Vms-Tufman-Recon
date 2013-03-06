using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recon.Web.Tests.TestHelper;
using Recon.Dal.Repository;
using Recon.Domain.Users;

namespace Recon.Test.Dal.Map.Users
{
    [TestClass]
    public class RoleMapTest
    {
        [TestMethod]
        public void Simple_Map_Test()
        {
            UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
            Repository repo = new Repository(unitOfWork.Session);
            List<Role> list = repo.GetAll<Role>().ToList();
            Assert.AreEqual(3, list.Count);
            unitOfWork.Rollback();
            unitOfWork.Dispose();
        }

        public void New_Role_Add_User_Users_Count_Equals_1()
        {
            UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
            Repository repo = new Repository(unitOfWork.Session);
            Role role = new Role
            {
                Id=99,
                Label="New Role"
            };
            repo.Save<Role>(role);

            User user = new User
            {
                Id = 99,
                Name = "Nom",
                Email = "Nom@gmail.com"
            };
            repo.Save<User>(user);


            List<Role> list = repo.GetAll<Role>().ToList();
            Assert.AreEqual(3, list.Count);
            unitOfWork.Rollback();
            unitOfWork.Dispose();
        }


    }
}
