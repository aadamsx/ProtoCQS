using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Decorated.Caching
{
    using System;
    using System.Collections.Generic;

    namespace CacheDiSample.Domain.CacheInterfaces
    {
        //public interface ICacheProvider<T>
        //{
        //    T Fetch(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry);
        //    T Fetch(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry, IEnumerable<ICacheDependency> cacheDependencies);

        //    IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry);
        //    IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry, IEnumerable<ICacheDependency> cacheDependencies);

        //    U CreateCacheDependency<U>()
        //        where U : ICacheDependency;
        //}
    }
}
