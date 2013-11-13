namespace Repository
{
    public interface IDeleteRepository<in TEntity> where TEntity : class
    {
        //void Delete(object id);
        void Submit(TEntity entity);
    }
}