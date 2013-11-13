using System;
using System.Data.Entity;
using AspNetIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Postal;
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

            //container.Register<IEmailService, EmailService>();

            // input variables on the left side
            // lambda body on the right side
            // Lambda expressions are inline expressions similar to anonymous methods but more flexible
            // The => operator has the same precedence as the assignment operator (=) and is right-associative
            container.Register <IEmailService>(() => new EmailService());

            // could be registers like so:
            //container.Register<IUserStore<ApplicationUser>>(() =>
            //    new UserStore<ApplicationUser>((DbContext)new SecurityContext()));


            //container.Register<IWebSecurity, WebSecurity>();

            // or registered as open generic type:
            //container.RegisterOpenGeneric(typeof(IUserStore<>), typeof(UserStore<>));
            //container.Register<IInterfaceOfApplicationDbContext>(() => new ApplicationDbContext());

        }
    }
}
