using System;
using System.Diagnostics;

namespace Repository.Decorated
{
    public abstract class DecoratedDeleteRepository<TEntity> 
        : IDeleteRepository<TEntity> where TEntity : class
    {
        private readonly IDeleteRepository<TEntity> _deleteRepository;
        protected DecoratedDeleteRepository(IDeleteRepository<TEntity> deleteRepository)
        {
            if (deleteRepository == null) throw new ArgumentNullException("deleteRepository");
            _deleteRepository = deleteRepository;
        }

        [DebuggerStepThrough]
        public virtual void Submit(TEntity entity)
        {
            _deleteRepository.Submit(entity);
        }
    }
}