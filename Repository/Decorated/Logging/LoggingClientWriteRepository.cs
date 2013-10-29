using Core.Helper;
using DataModel;
using Logger;

namespace Repository.Decorated.Logging_Depreciated
{
    public class LoggingClientWriteRepository 
        : DecoratedWriteRepository<Tenant> 
    {
        public LoggingClientWriteRepository(IWriteRepository<Tenant> writeRepository)
            : base(writeRepository)
        {
        }

        public override void Create(Tenant entity)
        {
            Check.Argument.IsNotNull(entity, "Tenant");

            Log.Info("Adding tenant: {0}, {1}", entity.TenantId, entity.Name);

            base.Create(entity);

            Log.Info("Tenant added: {0}, {1}", entity.TenantId, entity.Name);
        }

        public override void Delete(Tenant entity)
        {
            Check.Argument.IsNotNull(entity, "entity");

            Log.Info("Removing tenant: {0}, {1}", entity.TenantId, entity.Name);

            base.Delete(entity);

            Log.Info("Tenant removed: {0}, {1}", entity.TenantId, entity.Name);
        }

        public override void Update(Tenant entity)
        {
            Check.Argument.IsNotNull(entity, "entity");

            Log.Info("Updating tenant: {0}, {1}", entity.TenantId, entity.Name);

            base.Update(entity);

            Log.Info("Tenant updated: {0}, {1}", entity.TenantId, entity.Name);
        }
    }
}