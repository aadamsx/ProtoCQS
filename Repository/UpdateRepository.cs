using System.Data.Entity;
using Data;

namespace Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class UpdateRepository<TEntity> 
        : IUpdateRepository<TEntity> where TEntity : class
    {
        private readonly ClientManagementContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UpdateRepository(ClientManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Submit(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
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
    }
}