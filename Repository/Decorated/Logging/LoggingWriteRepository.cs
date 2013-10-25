using Helper;
using Logger;

namespace Repository.Decorated.Logging
{
    public class LoggingWriteRepository<TEntity> 
        : DecoratedWriteRepository<TEntity> where TEntity : class
    {
        public LoggingWriteRepository(IWriteRepository<TEntity> writeRepository) 
            : base(writeRepository)
        {
        }

        public override void Create(TEntity entity)
        {
            Check.Argument.IsNotNull(entity, "Tenant");

            Log.Info("Adding entity: {0}", entity.ToString());

            base.Create(entity);

            Log.Info("Entity added: {0}", entity.ToString());
        }

        public override void Delete(TEntity entity)
        {
            Check.Argument.IsNotNull(entity, "entity");

            Log.Info("Removing entity: {0}", entity.ToString());

            base.Delete(entity);

            Log.Info("Entity removed: {0}", entity.ToString());
        }

        public override void Update(TEntity entity)
        {
            Check.Argument.IsNotNull(entity, "entity");

            Log.Info("Updating entity: {0}", entity.ToString());

            base.Update(entity);

            Log.Info("Entity updated: {0}", entity.ToString());
        }
    }
}