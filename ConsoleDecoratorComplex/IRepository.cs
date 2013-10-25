using System.Collections.Generic;

namespace ProtoConsole
{
    public interface IRepository<T>
    {
        T ReadTById(object id);
        IEnumerable<T> ReadTs();
        void UpdateT(T entity);
        void CreateT(T entity);
        void DeleteT(T entity);
    }
}