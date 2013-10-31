using System;
using Data.Infrastructure;
using SimpleInjector;

namespace Data
{
    public static class DataBoostrapper
    {
        public static void Bootstrap(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }


            // could be registers like so:
            //container.Register<IContextConfiguration>(() => new ContextConfiguration());

            // or registered as open generic type:
            //container.RegisterOpenGeneric(typeof(IUserStore<>), typeof(UserStore<>));
            //container.Register<IInterfaceOfApplicationDbContext>(() => new ApplicationDbContext());

        }
    }
}
