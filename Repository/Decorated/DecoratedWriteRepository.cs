using System;
using System.Diagnostics;

namespace Repository.Decorated
{
    public abstract class DecoratedWriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
    {
        private readonly IWriteRepository<TEntity> _writeRepository;
        private readonly Guid _instanceId;
        protected DecoratedWriteRepository(IWriteRepository<TEntity> writeRepository)
        {
            if (writeRepository == null) throw new ArgumentNullException("writeRepository");

            _writeRepository = writeRepository;
            _instanceId = Guid.NewGuid();
        }

        [DebuggerStepThrough]
        public virtual void Create(TEntity entity)
        {
            _writeRepository.Create(entity);
        }

        [DebuggerStepThrough]
        public virtual void Update(TEntity entity)
        {
            _writeRepository.Update(entity);
        }

        [DebuggerStepThrough]
        public virtual void Delete(TEntity entity)
        {
            _writeRepository.Delete(entity);
        }
    }
}

