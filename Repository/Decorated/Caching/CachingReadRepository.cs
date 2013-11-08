using System.Collections.Generic;
using Caching;
using Core.Helper;

namespace Repository.Decorated.Caching
{
    /// <summary>
    /// Repository level cache
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CachingReadRepository<TEntity> : DecoratedReadRepository<TEntity> where TEntity : class
    {
        private readonly float _cacheDurationInMinutes;

        public CachingReadRepository(IReadRepository<TEntity> readRepository)
            : base(readRepository)
        {
            _cacheDurationInMinutes = 60;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            // might NEED to be immutable
            var cacheKey = string.Format("Repository.GetAll.{0}", typeof(TEntity).Name);

            IEnumerable<TEntity> result;

            Cache.TryGet(cacheKey, out result);

            if (result == null)
            {
                result = base.GetAll();

                if ((result != null)/* && (!CacheObject.Contains(cacheKey))*/)
                {
                    Cache.Set(cacheKey, result, SystemTime.Now().AddMinutes(_cacheDurationInMinutes));
                }
            }

            return result;
        }
    }
}