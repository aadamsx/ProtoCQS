namespace Repository
{
    public interface ICreateRepository<TEntity> where TEntity : class
    {
        void Submit(TEntity entity);
    }
}
