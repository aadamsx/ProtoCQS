using System;
using System.Diagnostics;

namespace Repository.Decorated
{
    public abstract class DecoratedCreateRepository<TEntity> 
        : ICreateRepository<TEntity> where TEntity : class
    {
        private readonly ICreateRepository<TEntity> _createRepository;
        protected DecoratedCreateRepository(ICreateRepository<TEntity> createRepository)
        {
            if (createRepository == null) throw new ArgumentNullException("createRepository");
            _createRepository = createRepository;
        }

        [DebuggerStepThrough]
        public virtual void Submit(TEntity entity)
        {
            _createRepository.Submit(entity);
        }
    }
}

