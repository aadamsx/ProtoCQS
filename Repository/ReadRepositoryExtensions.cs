using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Proto.Model.Entities;

namespace Repository
{
    public static class ReadRepositoryExtensions
    {
        // http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=84
        //public static Tenant GetById(
        //    this IQueryable<Tenant> repository, int id)
        //{
        //    return GetSingle(repository, e => e.TenantId == id, id);
        //}
        public static Tenant GetById(
            this IQueryable<Tenant> repository, int id)
        {
            return GetSingle(repository, e => e.TenantId == id, id);
        }

        public static Tenant GetLast(
            this IQueryable<Tenant> repository, int id)
        {
            return GetLast(repository, e => e.TenantId == id, id);
        }

        // TODO: More GetById methods here.

        private static T GetLast<T>(IQueryable<T> collection, 
            Expression<Func<T, bool>> predicate, object id)
            where T : class
        {
            T entity;

            try
            {
                entity = collection.OrderByDescending(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(
                    "There was an error retrieving an {0} with " +
                    "id {1}. {2}",
                    typeof(T).Name, id ?? "{null}", ex.Message), ex);
            }

            if (entity == null)
            {
                throw new KeyNotFoundException(string.Format(
                    "{0} with id {1} was not found.",
                    typeof(T).Name, id ?? "{null}"));
            }

            return entity;
        }

        // Allow reporting more descriptive error messages.
        private static T GetSingle<T>(IQueryable<T> collection,
            Expression<Func<T, bool>> predicate, object id)
            where T : class
        {
            T entity;

            try
            {
                entity = collection.SingleOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(
                    "There was an error retrieving an {0} with " +
                    "id {1}. {2}",
                    typeof(T).Name, id ?? "{null}", ex.Message), ex);
            }

            if (entity == null)
            {
                throw new KeyNotFoundException(string.Format(
                    "{0} with id {1} was not found.",
                    typeof(T).Name, id ?? "{null}"));
            }

            return entity;
        }

        // TODO: Empliment these at some point
        //public TEntity GetFirst()
        //{
        //    // Added AsNoTracking for performance and the disconnected state of MVC?
        //    var entity = context.Set<TEntity>().AsNoTracking().FirstOrDefault();
        //    if (entity == null) return null;
        //    return entity;
        //}

        //public TEntity GetNext()
        //{
        //    //var entity = (context.Set<TEntity>().Where(u => u.Id > id)).FirstOrDefault();
        //    //if (entity == null) return null;
        //    //return entity;
        //    throw new NotImplementedException();
        //}

        //public TEntity GetPrevious()
        //{
        //    //var entity = (from u in context.Set<TEntity>()
        //    //              where u.Id < id
        //    //              orderby u.Id descending
        //    //              select u).FirstOrDefault();
        //    //if (entity == null) return GetFirst();
        //    //return entity;

        //    throw new NotImplementedException();
        //}
        
        //public int GetMaxId()
        //{
        //    var max = context.Set<TEntity>().Count() + 1;
        //    return max;
        //}
    }
}