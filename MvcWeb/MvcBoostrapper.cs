using System;
using IdentityUserData;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace MvcWeb
{
    public static class MvcBoostrapper
    {
        public static void Bootstrap(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }


            // could be registers like so:
            container.Register<IUserStore<ApplicationUser>>(() =>
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            // or registered as open generic type:
            //container.RegisterOpenGeneric(typeof(IUserStore<>), typeof(UserStore<>));
            //container.Register<IInterfaceOfApplicationDbContext>(() => new ApplicationDbContext());

        }
    }
}
