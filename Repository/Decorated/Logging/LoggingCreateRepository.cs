using Core.Helper;
using Logger;

namespace Repository.Decorated.Logging
{
    public class LoggingCreateRepository<TEntity> 
        : DecoratedCreateRepository<TEntity> where TEntity : class
    {
        public LoggingCreateRepository(ICreateRepository<TEntity> createRepository) 
            : base(createRepository)
        {
        }

        public override void Submit(TEntity entity)
        {
            Check.Argument.IsNotNull(entity, "Tenant");

            Log.Info("Adding entity: {0}", entity.ToString());

            base.Submit(entity);

            Log.Info("Entity added: {0}", entity.ToString());
        }
    }
}