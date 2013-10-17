namespace Repository
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        //void InsertGraph(TEntity entity);
        void Create(TEntity entity);
        void Update(TEntity entity);
        //void Delete(object id);
        void Delete(TEntity entity);
    }
}
