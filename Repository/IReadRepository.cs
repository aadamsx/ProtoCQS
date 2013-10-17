using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);
        IQueryable<TEntity> GetAll(TEntity entity);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);

        IRepositoryQuery<TEntity> Query();
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

    }

    public interface IRepositoryQuery<T>
    {
    }
}
