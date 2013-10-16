using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Proto.Mvc.Mgmt.App_Start;
using Proto.Mvc.Mgmt.Mappers;

namespace Proto.Mvc.Mgmt
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/fwlink/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SimpleInjectorInitializer.Initialize();

            AutoMapperConfiguration.Configure();
        }
    }
}
