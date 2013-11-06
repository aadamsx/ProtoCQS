namespace Repository
{
    public interface IUpdateRepository<TEntity> where TEntity : class
    {
        void Submit(TEntity entity);
    }
}