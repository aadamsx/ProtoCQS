using SimpleInjector;
using SimpleInjector.Extensions;

namespace ProtoConsole
{
    public static class PrototypeBoostrapper
    {
        public static void Bootstrap(Container container)
        {
            container.RegisterOpenGeneric(
                typeof(IRepository<>),
                typeof(Repository<>));

            container.RegisterDecorator(
                typeof(IRepository<>),
                typeof(LoggingDecorator<>));

            container.RegisterDecorator(
                typeof(IRepository<>),
                typeof(ValidateUserDecorator<>));
            
        }
    }
}