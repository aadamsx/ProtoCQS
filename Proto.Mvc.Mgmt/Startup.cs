using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proto.Mvc.Mgmt.Startup))]
namespace Proto.Mvc.Mgmt
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
