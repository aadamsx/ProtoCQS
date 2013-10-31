using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Decorated
{
    public abstract class DecoratedReadRepository<TEntity> 
        : IReadRepository<TEntity> where TEntity : class
    {
        private readonly IReadRepository<TEntity> _readRepository;
        protected readonly Guid InstanceId;
        protected DecoratedReadRepository(IReadRepository<TEntity> readRepository)
        {
            if (readRepository == null) throw new ArgumentNullException("readRepository");
            _readRepository = readRepository;
            InstanceId = Guid.NewGuid();
        }

        [DebuggerStepThrough]
        public virtual TEntity GetById(object id)
        {
            return _readRepository.GetById(id);
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _readRepository.GetAll();
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _readRepository.Find(predicate);
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return _readRepository.SqlQuery(query, parameters);
        }

        [DebuggerStepThrough]
        public virtual IRepositoryQuery<TEntity> Query()
        {
            return _readRepository.Query();
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, 
            List<Expression<Func<TEntity, object>>> includeProperties = null, 
            int? page = null,
            int? pageSize = null)
        {
            return _readRepository.Get(filter, orderBy, includeProperties, page, pageSize);
        }

        
    }
}