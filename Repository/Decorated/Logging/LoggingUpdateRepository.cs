using Core.Helper;
using Logger;

namespace Repository.Decorated.Logging
{
    public class LoggingUpdateRepository<TEntity>
        : DecoratedUpdateRepository<TEntity> where TEntity : class
    {
        public LoggingUpdateRepository(IUpdateRepository<TEntity> updateRepository)
            : base(updateRepository)
        {
        }

        public override void Submit(TEntity entity)
        {
            Check.Argument.IsNotNull(entity, "entity");

            Log.Info("Updating entity: {0}", entity.ToString());

            base.Submit(entity);

            Log.Info("Entity updated: {0}", entity.ToString());
        }
    }
}