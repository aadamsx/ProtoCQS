using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Data;
using DataModel;

namespace Repository
{
    /// <summary>
    /// If you want better performance, you can use MyContext.Database.SqlCommand("your query"); or MyContext.ExecuteStoreQuery("your query")
    /// to call out to a stored procedure.  But then again, don't do it here.  This is for basic Reads (and Queries) that do not take into
    /// account performance.  Either extend this or use a Query Handler for Stored Procedures and Performance considered objects. 
    /// 
    /// 
    /// The query is not executed in the database until you invoke ToList(), FirstOrDefault() etc. So if you want to be 
    /// able to keep all data related exceptions in the repositories you have to invoke those methods.
    /// 
    /// There are no complete LINQ to SQL implementations. They all are either missing features or 
    /// implement things like eager/lazy loading in their own way. That means that they all are 
    /// leaky abstractions. So if you expose LINQ outside your repository you get a leaky abstraction. 
    /// You could really stop using the repository pattern then and use the OR/M directly.
    /// http://blog.gauffin.org/2013/01/repository-pattern-done-right/#.UmBpPFCfgSQ
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ReadRepository<TEntity> 
        : IReadRepository<TEntity> where TEntity : class
    {

        //private readonly Guid _instanceId;
        private readonly ClientManagementContext Context;

        public ReadRepository(ClientManagementContext context)
        {
            Context = context;
            //dbSet = this.context.Set<TEntity>();
            //InstanceId = Guid.NewGuid();

            // set these properties for faster performance
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
        }

        //public Guid InstanceId
        //{
        //    get { return _instanceId; }
        //}

        public virtual TEntity GetById(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            // As no tracking prevents entities returned from being cached in the dbcontext
            return Context.Set<TEntity>().AsNoTracking().AsEnumerable();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            // As no tracking prevents entities returned from being cached in the dbcontext
            return Context.Set<TEntity>().Where(predicate).AsNoTracking().AsEnumerable();
        }

        public virtual IRepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<TEntity>(this);
            return repositoryGetFluentHelper;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, 
            List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

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
            var results = query.AsNoTracking().AsEnumerable();

            return results;
        }


        public virtual IEnumerable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return Context.Set<TEntity>().SqlQuery(query, parameters).AsNoTracking().AsEnumerable();
        }
    }
}