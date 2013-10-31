using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data;

namespace Repository
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> SqlQuery(string query, params object[] parameters);

        IRepositoryQuery<TEntity> Query();
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

        //ClientManagementContext Context { get; }
    }
}
