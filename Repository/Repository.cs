using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Proto.Model.Entities;

namespace Repository
{
    /// <summary>
    /// http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=84
    /// The sole reason to have a Repository<T> instead of simply returning 
    /// IQueryable<T> is because of the InsertOnSubmit and DeleteOnSubmit methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public abstract class Repository<T> : IQueryable<T>
    //    where T : class
    //{
    //    private readonly IQueryable<T> query;

    //    protected Repository(IQueryable<T> query)
    //    {
    //        this.query = query;
    //    }

    //    public Type ElementType
    //    {
    //        get { return this.query.ElementType; }
    //    }

    //    public Expression Expression
    //    {
    //        get { return this.query.Expression; }
    //    }

    //    public virtual IQueryProvider Provider
    //    {
    //        get { return this.query.Provider; }
    //    }

    //    public abstract void InsertOnSubmit(T entity);

    //    public abstract void DeleteOnSubmit(T entity);

    //    public void InsertAllOnSubmit(IEnumerable<T> entities)
    //    {
    //        foreach (var entity in entities)
    //        {
    //            this.InsertOnSubmit(entity);
    //        }
    //    }

    //    public void DeleteAllOnSubmit(IEnumerable<T> entities)
    //    {
    //        foreach (var entity in entities)
    //        {
    //            this.DeleteOnSubmit(entity);
    //        }
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return this.query.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return this.query.GetEnumerator();
    //    }
    //}

    //public static class RepositoryExtensions
    //{
    //    public static Tenant GetById(
    //        this IQueryable<Tenant> repository, int id)
    //    {
    //        return GetSingle(repository, e => e.TenantId == id, id);
    //    }

    //    //public static Employee GetById(
    //    //    this IQueryable<Employee> repository, int id)
    //    //{
    //    //    return GetSingle(repository, e => e.Id == id, id);
    //    //}

    //    //public static Order GetById(
    //    //    this IQueryable<Order> repository, int id)
    //    //{
    //    //    return GetSingle(repository, e => e.Id == id, id);
    //    //}

    //    // TODO: More GetById methods here.

    //    // Allow reporting more descriptive error messages.
    //    /// <summary>
    //    /// While it would be sufficient to implement those GetById methods with return 
    //    /// repository.Single(e => e.Id == id), I found out quickly that this would give 
    //    /// very little information on failure. A private method that throws a more 
    //    /// descriptive exception was the solution.
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="collection"></param>
    //    /// <param name="predicate"></param>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    private static T GetSingle<T>(IQueryable<T> collection,
    //        Expression<Func<T, bool>> predicate, object id)
    //        where T : class
    //    {
    //        T entity;

    //        try
    //        {
    //            entity = collection.SingleOrDefault(predicate);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new InvalidOperationException(string.Format(
    //                "There was an error retrieving an {0} with " +
    //                "id {1}. {2}",
    //                typeof(T).Name, id ?? "{null}", ex.Message), ex);
    //        }

    //        if (entity == null)
    //        {
    //            throw new KeyNotFoundException(string.Format(
    //                "{0} with id {1} was not found.",
    //                typeof(T).Name, id ?? "{null}"));
    //        }

    //        return entity;
    //    }
    //}
}
