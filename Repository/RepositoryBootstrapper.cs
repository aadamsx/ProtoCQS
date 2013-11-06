using System;
using Repository.Decorated.Logging;
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

            container.RegisterOpenGeneric(
                typeof(ICreateRepository<>),
                typeof(CreateRepository<>));
            container.RegisterOpenGeneric(
                typeof(IReadRepository<>),
                typeof(ReadRepository<>));
            container.RegisterOpenGeneric(
                typeof(IUpdateRepository<>),
                typeof(UpdateRepository<>));
            container.RegisterOpenGeneric(
                typeof(IDeleteRepository<>),
                typeof(DeleteRepository<>));

            container.RegisterOpenGeneric(
                typeof(IRepositoryQuery<>),
                typeof(RepositoryQuery<>));

            container.RegisterDecorator(
                typeof(ICreateRepository<>),
                typeof(LoggingCreateRepository<>));
            container.RegisterDecorator(
                typeof(IReadRepository<>), 
                typeof(LoggingReadRepository<>));
            container.RegisterDecorator(
                typeof(IUpdateRepository<>),
                typeof(LoggingUpdateRepository<>));
            container.RegisterDecorator(
                typeof(IDeleteRepository<>),
                typeof(LoggingDeleteRepository<>));


            //container.Register(typeof (ITenantReadRepository), typeof (TenantReadRepository));

            //container.RegisterDecorator(typeof(IReadRepository<>), typeof(LoggingClientReadRepository));
            //container.RegisterDecorator(typeof(IWriteRepository<>), typeof(LoggingClientWriteRepository));

            // First pass at wirting up DI failed:




            //container.RegisterDecorator(
            //    typeof(IReadRepository<>),
            //    typeof(DecoratedReadRepository<>));
            //container.RegisterDecorator(
            //    typeof(IWriteRepository<>),
            //    typeof(DecoratedWriteRepository<>));

            //container.RegisterDecorator(
            //    typeof(DecoratedReadRepository<>),
            //    typeof(LoggingReadRepository<>));
            //container.RegisterDecorator(
            //    typeof(DecoratedWriteRepository<>),
            //    typeof(LoggingWriteRepository<>));

            //container.RegisterDecorator(
            //    typeof(DecoratedReadRepository<>),
            //    typeof(LoggingClientReadRepository<Tenant>));
            //container.RegisterDecorator(
            //    typeof(DecoratedWriteRepository<>),
            //    typeof(LoggingClientWriteRepository<>));
        }
    }
}
