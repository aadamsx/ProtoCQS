using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataModel;
using Helper;
using Logger;

namespace Repository.Decorated.Logging
{
    public class LoggingClientReadRepository
        : DecoratedReadRepository<Tenant>
    {
        public LoggingClientReadRepository(IReadRepository<Tenant> readRepository)
            : base(readRepository) { }

        public override Tenant GetById(object id)
        {
            Check.Argument.IsNotEmpty((string)id, "id");

            Log.Info("Retrieving tenants with id: {0}", id);

            var result = base.GetById(id);

            if (result == null)
            {
                Log.Warning("Did not find any tenants with id: {0}", id);
            }
            else
            {
                Log.Info("Tenant retrieved with id: {0}", id);
            }

            return result;
        }

        public override IEnumerable<Tenant> GetAll()
        {
            Log.Info("Retrieving all tenants");

            var result = base.GetAll();

            if (result == null)
            {
                Log.Warning("Did not find any tenants");
            }
            else
            {
                Log.Info("Tenants retrieved");
            }

            return result;
        }

        public override IEnumerable<Tenant> Find(Expression<Func<Tenant, bool>> predicate)
        {
            Log.Info("Retrieving tenants with predicate: {0}", predicate.ToString());

            var result = base.Find(predicate);

            if (result == null)
            {
                Log.Warning("Did not find any tenants with predicate: {0}", predicate.ToString());
            }
            else
            {
                Log.Info("Tenants retreived with predicate: {0}", predicate.ToString());
            }

            return result;
        }

        public override IEnumerable<Tenant> SqlQuery(string query, params object[] parameters)
        {
            Log.Info("Retrieving tenants with query: {0}, {1}", query, parameters.ToString());

            var result = base.SqlQuery(query, parameters);

            if (result == null)
            {
                Log.Warning("Did not find tenants with query: {0}, {1}", query, parameters.ToString());
            }
            else
            {
                Log.Info("Tenants retrieved with query: {0}, {1}", query, parameters.ToString());
            }

            return result;
        }

        public override IRepositoryQuery<Tenant> Query()
        {
            Log.Info("Querying tenants");

            var result = base.Query();

            if (result == null)
            {
                Log.Warning("Did not find tenants query");
            }
            else
            {
                Log.Info("Tenants retrieved with query");
            }

            return result;
        }

        public override IEnumerable<Tenant> Get(
            Expression<Func<Tenant, bool>> filter = null,
            Func<IQueryable<Tenant>,
                IOrderedQueryable<Tenant>> orderBy = null,
            List<Expression<Func<Tenant, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            Log.Info("Retrieving tenants with unique specifications: {0}, {1}, {2}, {3}, {4}",
                (filter != null ? filter.ToString() : ""),
                (orderBy != null ? orderBy.ToString() : ""),
                (includeProperties != null ? includeProperties.ToString() : ""),
                (page != null ? page.ToString() : ""),
                (pageSize != null ? pageSize.ToString() : ""));

            var result = base.Get(filter, orderBy, includeProperties, page, pageSize);

            if (result == null)
            {
                Log.Warning("Did not find tenants with unique specifications");
            }
            else
            {
                Log.Info("Tenants retrieved with specifications");
            }

            return result;
        }
    }
}