using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
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

            Type t = typeof(TEntity);
            var name = t.Name.ToString(CultureInfo.InvariantCulture);

            Log.Info("Retrieving {0} with id: {1}", name, id);

            var result = base.GetById(id);

            if (result == null)
            {
                Log.Warning("Did not find {0} with id: {1}", name, id);
            }
            else
            {
                Log.Info("{0} retrieved with id: {1}", name, id);
            }

            return result;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            Type t = typeof (TEntity);
            var name = t.Name.ToString(CultureInfo.InvariantCulture);

            Log.Info("Retrieving all {0}s", name);

            var result = base.GetAll();

            if (result == null)
            {
                Log.Warning("Did not find any {0}s", name);
            }
            else
            {
                Log.Info("{0}s retrieved", name);
            }

            return result;
        }

        public override IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            Type t = typeof(TEntity);
            var name = t.Name.ToString(CultureInfo.InvariantCulture);

            Log.Info("Retrieving {0}s with predicate: {1}", name, predicate.ToString());

            var result = base.Find(predicate);

            if (result == null)
            {
                Log.Warning("Did not find any {0}s with predicate: {1}", name, predicate.ToString());
            }
            else
            {
                Log.Info("{0}s retreived with predicate: {1}", name, predicate.ToString());
            }

            return result;
        }

        public override IEnumerable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            Type t = typeof(TEntity);
            var name = t.Name.ToString(CultureInfo.InvariantCulture);

            Log.Info("Retrieving {0}s with query: {1}, {2}", name, query, parameters.ToString());

            var result = base.SqlQuery(query, parameters);

            if (result == null)
            {
                Log.Warning("Did not find {0}s with query: {1}, {2}", name, query, parameters.ToString());
            }
            else
            {
                Log.Info("{0}s retrieved with query: {1}, {2}", name, query, parameters.ToString());
            }

            return result;
        }

        public override IRepositoryQuery<TEntity> Query()
        {
            Type t = typeof(TEntity);
            var name = t.Name.ToString(CultureInfo.InvariantCulture);

            Log.Info("Querying for {0}s", name);

            var result = base.Query();

            if (result == null)
            {
                Log.Warning("Did not find {0}s with query", name);
            }
            else
            {
                Log.Info("{0}s retrieved with query", name);
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

            Type t = typeof(TEntity);
            var name = t.Name.ToString(CultureInfo.InvariantCulture);

            var result = base.Get(filter, orderBy, includeProperties, page, pageSize);

            if (result == null)
            {
                Log.Warning("Did not find {0}s with unique specifications", name);
            }
            else
            {
                Log.Info("{0}s retrieved with specifications", name);
            }

            return result;
        }
    }
}