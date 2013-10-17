using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Proto.Data;

namespace Repository
{
    /// <summary>
    /// If you want better performance, you can use MyContext.Database.SqlCommand("your query"); or MyContext.ExecuteStoreQuery("your query")
    /// to call out to a stored procedure.  But then again, don't do it here.  This is for basic Reads (and Queries) that do not take into
    /// account performance.  Either extend this or use a Query Handler for Stored Procedures and Performance considered objects. 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ReadRepository<TEntity> 
        : IReadRepository<TEntity> where TEntity : class
    {

        private readonly Guid instanceId;
        private ClientManagementContext context;

        protected ReadRepository(ClientManagementContext context)
        {
            this.context = context;
            //dbSet = this.context.Set<TEntity>();
            instanceId = Guid.NewGuid();

            // set these properties for faster performance
            this.context.Configuration.AutoDetectChangesEnabled = false;
            this.context.Configuration.ValidateOnSaveEnabled = false;
        }

        public Guid InstanceId
        {
            get { return instanceId; }
        }

        public TEntity GetById(object id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll(TEntity entity)
        {
            return context.Set<TEntity>();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public Repository.IRepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<TEntity>(this);
            return repositoryGetFluentHelper;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (includeProperties != null)
                includeProperties.ForEach(i => query = query.Include(i));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            // Added AsNoTracking to improve performance -- This will be a MVC client so it's detached anyhow?
            var results = query.AsNoTracking();

            return results;
        }

        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return context.Set<TEntity>().SqlQuery(query, parameters).AsQueryable();
        }
    }
}