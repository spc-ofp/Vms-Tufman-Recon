using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NHibernate;
using Recon.Web.NhibernateHelper;

namespace Recon.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ISessionFactory _sessionFactory = CreateSessionFactory();

        protected static ISessionFactory CreateSessionFactory()
        {
             return NHibernateHelper.SessionFactory;
        }

        public static IUnitOfWork UnitOfWork
        {
            get { return (IUnitOfWork)HttpContext.Current.Items["current.uow"]; }
            set { HttpContext.Current.Items["current.uow"] = value; }
        }

        protected MvcApplication()
        {
            // This configures the unit or work to be on a per request basis.
            //
            BeginRequest += delegate
            {
                UnitOfWork = new UnitOfWork(_sessionFactory);
            };
            EndRequest += delegate
            {
                if (UnitOfWork != null)
                {
                    // Notice that we are rolling back unless
                    //    an explicit call to commit was made elsewhere.
                    //
                    UnitOfWork.Dispose();
                }
            };
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}