using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;

namespace Recon.Web.Tests.TestHelper
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            IPersistenceConfigurer cfg =
                MsSqlConfiguration.MsSql2008.ConnectionString(c => c
                        .FromConnectionStringWithKey("Recon")).ShowSql();

            return Fluently.Configure()
                .Database(cfg)
                .Mappings(m =>
                {
                    m.FluentMappings
                        .AddFromAssemblyOf<NHibernateHelper>();
                    ;
                })
                .BuildSessionFactory();
        }
    }
}
