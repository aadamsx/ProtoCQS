using System;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace Repository
{
    // This class allows registering all types that are defined in the business layer, and are shared across
    // all applications that use this layer (WCF and Web API). For simplicity, this class is placed inside this
    // assembly, but this does couple the business layer assembly to the used container. If this is a concern,
    // create a specific BusinessLayer.Bootstrap project with this class.
    public static class RepositoryBoostrapper
    {
        public static void Bootstrap(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            container.RegisterOpenGeneric(typeof(IRepositoryQuery<>), typeof(RepositoryQuery<>));
            container.RegisterOpenGeneric(typeof(IReadRepository<>), typeof(ReadRepository<>));
            container.RegisterOpenGeneric(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        }
    }
}
