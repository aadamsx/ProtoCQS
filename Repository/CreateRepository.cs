using System;
using System.Data.Entity;
using Data;

namespace Repository
{
    /// <summary>
    /// since this is CUD -- No Reads here, we are not going to be dealing with Entity graphs or Aggreate Root here, so just 
    /// call save changes on everything (no need to take into account other entites saving all at at once?  For more complex 
    /// Add() operations, then don't use this Generic Repo, extend this or use a Command Handler
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CreateRepository<TEntity> 
        : ICreateRepository<TEntity> where TEntity : class
    {
        //private readonly Guid instanceId;
        //private DbSet<TEntity> dbSet;
        private ClientManagementContext context;
        public CreateRepository(ClientManagementContext context)
        {
            //if (context == null) throw new ArgumentNullException("context");
            this.context = context;
            //dbSet = context.Set<TEntity>();
            //instanceId = Guid.NewGuid();

            // Note: if I try to do a bulk insert with the entity framework that it does use 1 insert per data row inserted
            // (thus a roundtrip for each insert). And thus that the performance is less than using an SQL "insert"
            // set these properties for faster performance
            this.context.Configuration.AutoDetectChangesEnabled = false;
            this.context.Configuration.ValidateOnSaveEnabled = false;
        }

        /// <summary>
        /// And a private Save() method that returns true or false so 
        /// you can fallback easy in the controller depending on the result
        /// </summary>
        /// <returns></returns>
        private void Save()
        {
            context.SaveChanges();
        }

        public void Submit(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
            Save();
        }
    }
}