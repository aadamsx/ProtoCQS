using System;
using System.Diagnostics;

namespace Repository.Decorated
{
    public abstract class DecoratedUpdateRepository<TEntity> 
        : IUpdateRepository<TEntity> where TEntity : class
    {
        private readonly IUpdateRepository<TEntity> _updateRepository;
        protected DecoratedUpdateRepository(IUpdateRepository<TEntity> updateRepository)
        {
            if (updateRepository == null) throw new ArgumentNullException("updateRepository");
            _updateRepository = updateRepository;
        }

        [DebuggerStepThrough]
        public virtual void Submit(TEntity entity)
        {
            _updateRepository.Submit(entity);
        }
    }
}