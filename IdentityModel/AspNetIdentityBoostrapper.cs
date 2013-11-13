using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace AspNetIdentity
{
    public static class AspNetIdentityBoostrapper
    {
        public static void Bootstrap(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Register<IUserStore<ApplicationUser>>(() =>
                new UserStore<ApplicationUser>(new SecurityContext()));
            container.RegisterOpenGeneric(typeof(ISecurityRepository<>), typeof(SecurityRepository<>));
            container.Register<IWebSecurity, WebSecurity>();
        }
    }
}
