using System.Collections.Generic;
using Core.Helper;

namespace Repository.Decorated.Caching
{
    class CachingReadRepository<TEntity> : DecoratedReadRepository<TEntity> where TEntity : class
    {
        private readonly float _cacheDurationInMinutes;

        public CachingReadRepository(IReadRepository<TEntity> readRepository, float cacheDurationInMinutes)
            : base(readRepository)
        {
            _cacheDurationInMinutes = cacheDurationInMinutes;
            Check.Argument.IsNotNegativeOrZero(cacheDurationInMinutes, "cacheDurationInMinutes");

            _cacheDurationInMinutes = cacheDurationInMinutes;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            //const string CacheKey = "categories:All";

            //IEnumerable<TEntity> result;

            //Cache.TryGet(CacheKey, out result);

            //if (result == null)
            //{
            //    result = base.FindAll();

            //    if ((!result.IsNullOrEmpty()) && (!Cache.Contains(CacheKey)))
            //    {
            //        Cache.Set(CacheKey, result, SystemTime.Now().AddMinutes(_cacheDurationInMinutes));
            //    }
            //}

            return null;//result;
        }
    }
}