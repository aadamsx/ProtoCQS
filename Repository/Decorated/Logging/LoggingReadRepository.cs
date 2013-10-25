using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Helper;
using Logger;

namespace Repository.Decorated.Logging
{
    public class LoggingReadRepository<TEntity> 
        : DecoratedReadRepository<TEntity> where TEntity : class
    {
        public LoggingReadRepository(IReadRepository<TEntity> readRepository) 
            : base(readRepository) { }

        public override TEntity GetById(object id)
        {
            Check.Argument.IsNotEmpty(id.ToString(), "id");

            Log.Info("Retrieving entity with id: {0}", id);

            var result = base.GetById(id);

            if (result == null)
            {
                Log.Warning("Did not find any entity with id: {0}", id);
            }
            else
            {
                Log.Info("Entity retrieved with id: {0}", id);
            }

            return result;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            Log.Info("Retrieving all entities");

            var result = base.GetAll();

            if (result == null)
            {
                Log.Warning("Did not find any entities");
            }
            else
            {
                Log.Info("Entities retrieved");
            }

            return result;
        }

        public override IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            Log.Info("Retrieving entities with predicate: {0}", predicate.ToString());

            var result = base.Find(predicate);

            if (result == null)
            {
                Log.Warning("Did not find any entities with predicate: {0}", predicate.ToString());
            }
            else
            {
                Log.Info("Entities retreived with predicate: {0}", predicate.ToString());
            }

            return result;
        }

        public override IEnumerable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            Log.Info("Retrieving entities with query: {0}, {1}", query, parameters.ToString());

            var result = base.SqlQuery(query, parameters);

            if (result == null)
            {
                Log.Warning("Did not find entities with query: {0}, {1}", query, parameters.ToString());
            }
            else
            {
                Log.Info("Entities retrieved with query: {0}, {1}", query, parameters.ToString());
            }

            return result;
        }

        public override IRepositoryQuery<TEntity> Query()
        {
            Log.Info("Querying entities");

            var result = base.Query();

            if (result == null)
            {
                Log.Warning("Did not find entities query");
            }
            else
            {
                Log.Info("Entities retrieved with query");
            }

            return result;
        }

        public override IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, 
            List<Expression<Func<TEntity, object>>> includeProperties = null, 
            int? page = null,
            int? pageSize = null)
        {
            Log.Info("Retrieving entities with unique specifications: {0}, {1}, {2}, {3}, {4}", 
                (filter != null ? filter.ToString() : ""), 
                (orderBy != null ? orderBy.ToString() : ""), 
                (includeProperties != null ? includeProperties.ToString() : ""), 
                (page != null ? page.ToString() : ""), 
                (pageSize != null ? pageSize.ToString() : ""));

            var result = base.Get(filter, orderBy, includeProperties, page, pageSize);

            if (result == null)
            {
                Log.Warning("Did not find entities with unique specifications");
            }
            else
            {
                Log.Info("Entities retrieved with specifications");
            }

            return result;
        }
    }
}