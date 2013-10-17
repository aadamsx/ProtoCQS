// http://stackoverflow.com/questions/4128640/how-to-remove-unit-of-work-functionality-from-repositories-using-ioc

namespace Proto.Data.Infrastructure
{
    //// (1) Defining the factory
    //public interface IClientUnitOfWorkFactory
    //{
    //    ClientUnitOfWork CreateNew();
    //}

    //public partial class ClientUnitOfWork
    //{
    //}

    //// (2) Creating an abstract unit of work for the Client domain
    //// Remove partial once/if you move this to another file
    //public abstract partial class ClientUnitOfWork : IDisposable
    //{
    //    public IQueryable<Tenant> QueryableTenants
    //    {
    //        [DebuggerStepThrough]
    //        get { return this.GetRepository<Tenant>(); }
    //    }

    //    public IQueryable<Order> QueryableOrders
    //    {
    //        [DebuggerStepThrough]
    //        get { return this.GetRepository<Order>(); }
    //    }

    //    public abstract void Insert(object entity);

    //    public abstract void Delete(object entity);

    //    public abstract void SubmitChanges();

    //    public void Dispose()
    //    {
    //        this.Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected abstract IQueryable<T> GetRepository<T>()
    //        where T : class;

    //    protected virtual void Dispose(bool disposing) { }
    //}

    //public class Order
    //{
    //}

    //// (3) Create a concrete factory
    //public class LinqToSqlClientUnitOfWorkFactory : IClientUnitOfWorkFactory
    //{
    //    private static readonly MappingSource Mapping =
    //        new AttributeMappingSource();

    //    public string AcmeConnectionString { get; set; }

    //    public ClientUnitOfWork CreateNew()
    //    {
    //        var context = new DataContext(this.AcmeConnectionString, Mapping);
    //        return new LinqToSqlClientUnitOfWork(context);
    //    }
    //}

    //// The factory created a LinqToSqlClientUnitOfWork based on the ClientUnitOfWork base class
    //internal sealed class LinqToSqlClientUnitOfWork : ClientUnitOfWork
    //{
    //    private readonly DataContext db;

    //    public LinqToSqlClientUnitOfWork(DataContext db) { this.db = db; }

    //    public override void Insert(object entity)
    //    {
    //        if (entity == null) throw new ArgumentNullException("entity");
    //        this.db.GetTable(entity.GetType()).InsertOnSubmit(entity);
    //    }

    //    public override void Delete(object entity)
    //    {
    //        if (entity == null) throw new ArgumentNullException("entity");
    //        this.db.GetTable(entity.GetType()).DeleteOnSubmit(entity);
    //    }

    //    public override void SubmitChanges();
    //    {
    //        this.db.SubmitChanges();
    //    }

    //    protected override IQueryable<TEntity> GetRepository<TEntity>()
    //    {
    //        return this.db.GetTable<TEntity>();
    //    }

    //    protected override void Dispose(bool disposing) { this.db.Dispose(); }
    //}


}
