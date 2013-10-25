using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;

namespace ProtoConsole
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PrototypeContext _context;

        public Repository(PrototypeContext context)
        {
            _context = context;
        }

        public T ReadTById(object id) {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> ReadTs() {
            return _context.Set<T>().AsNoTracking().AsEnumerable(); 
        }

        public void UpdateT(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void CreateT(T entity) {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }


        public void DeleteT(T entity) {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}