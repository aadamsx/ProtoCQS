namespace Repository
{
    //public class BaseRepository<T, U> where T : class
    //{
    //    private readonly Guid instanceId;
    //    private ClientManagementContext context;

    //    protected BaseRepository(ClientManagementContext context)
    //    {
    //        this.context = context;
    //        //dbSet = this.context.Set<TEntity>();
    //        instanceId = Guid.NewGuid();

    //        // set these properties for faster performance
    //        this.context.Configuration.AutoDetectChangesEnabled = false;
    //        this.context.Configuration.ValidateOnSaveEnabled = false;
    //    }

    //    public virtual IQueryable<U> GetAll()
    //    {
    //        //using (ApplicationEntities context = new ApplicationEntities())
    //        //{
    //        IQueryable<T> models = context.Set<T>();
    //        return Mapper.Map<IQueryable<T>, IQueryable<U>>(models);
    //        //}
    //    }
    //}
}