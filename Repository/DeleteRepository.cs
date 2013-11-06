using System;
using System.Data.Entity;
using Data;

namespace Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    class DeleteRepository<TEntity> 
        : IDeleteRepository<TEntity> where TEntity : class
    {
        private readonly ClientManagementContext _context;

        public DeleteRepository(ClientManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// And a private Save() method that returns true or false so you 
        /// can fallback easy in the controller depending on the result
        /// </summary>
        /// <returns></returns>
        private void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Submit(object id)
        {
            // var entity = dbSet.Find(id);
            //dbSet.Remove(entity);
            // or 
            //context.Entry(entity).State = EntityState.Deleted;
            //Save();
            throw new NotImplementedException();
        }

        /// <summary>
        /// EF will automatically attach detached objects in the graph when 
        /// setting the state of an entity or when SaveChanges() is called
        /// </summary>
        /// <param name="entity"></param>
        public void Submit(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            Save();
        }
    }
}