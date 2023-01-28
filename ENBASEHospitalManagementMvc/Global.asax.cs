using ENBASEHospitalManagementMvc.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ENBASEHospitalManagementMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IDbConnectionFactory DefaultConnectionFactory { get; set; }
        protected void Application_Start()
        {
            // Use LocalDB for Entity Framework by default

            DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\\MSSQLLocalDB; Integrated Security=True; MultipleActiveResultSets=True");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerConfig.Configure();
        }
    }
}
