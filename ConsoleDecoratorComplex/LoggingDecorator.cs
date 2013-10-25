using System.Collections.Generic;
using System.Diagnostics;

namespace ProtoConsole
{
    public class LoggingDecorator<T> : IRepository<T>
    {
        private readonly IRepository<T> decoratee;
        private readonly ILogger logger;

        public LoggingDecorator(IRepository<T> decoratee, ILogger logger)
        {
            this.decoratee = decoratee;
            this.logger = logger;
        }

        public T ReadTById(object id)
        {
            return this.decoratee.ReadTById(id);
        }

        public IEnumerable<T> ReadTs()
        {
            return this.decoratee.ReadTs();
        }

        public void UpdateT(T entity)
        {
            var watch = Stopwatch.StartNew();

            this.decoratee.UpdateT(entity);

            this.logger.Log(typeof(T).Name + " executed in " +
                            watch.ElapsedMilliseconds + " ms.");
        }

        public void CreateT(T entity)
        {
            var watch = Stopwatch.StartNew();

            this.decoratee.CreateT(entity);

            this.logger.Log(typeof(T).Name + " executed in " +
                            watch.ElapsedMilliseconds + " ms.");
        }

        public void DeleteT(T entity)
        {
            this.decoratee.DeleteT(entity);
        }
    }
}