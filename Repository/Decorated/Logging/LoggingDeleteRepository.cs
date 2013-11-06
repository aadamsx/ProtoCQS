using Core.Helper;
using Logger;

namespace Repository.Decorated.Logging
{
    public class LoggingDeleteRepository<TEntity>
        : DecoratedDeleteRepository<TEntity> where TEntity : class
    {
        public LoggingDeleteRepository(IDeleteRepository<TEntity> deleteRepository)
            : base(deleteRepository) { }

        public override void Submit(TEntity entity)
        {
            Check.Argument.IsNotNull(entity, "entity");

            Log.Info("Removing entity: {0}", entity.ToString());

            base.Submit(entity);

            Log.Info("Entity removed: {0}", entity.ToString());
        }
    }
}