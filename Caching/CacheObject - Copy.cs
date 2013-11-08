//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Caching;
//using System.Text;
//using System.Threading.Tasks;

//namespace Caching
//{
//    /// <summary>
//    /// Object this cache behaviors.
//    /// </summary>
//    /// <typeparam name="T">The object type</typeparam>
//    public class CacheObject<T>
//    {
//        #region Ctor
//        /// <summary>
//        /// Constractor for CacheObject.
//        /// </summary>
//        /// <param name="getValueFunc">Function that returns the object (When the object is not in the cache, this method will be called)</param>
//        public CacheObject(Func<T> getValueFunc)
//            : this(30, getValueFunc)
//        {
//        }

//        /// <summary>
//        /// Constractor for CacheObject.
//        /// </summary>
//        /// <param name="cacheTimeoutMinutes">Expiration time(seconds) for cache</param>
//        /// <param name="getValueFunc">Function that returns the object (When the object is not in the cache, this method will be called)</param>
//        public CacheObject(int cacheTimeoutMinutes, Func<T> getValueFunc)
//        {
//            _cacheTimeoutMinutes = cacheTimeoutMinutes;
//            _cache = new MemoryCache(Guid.NewGuid().ToString());
//            _policy = new CacheItemPolicy();
//            _policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(_cacheTimeoutMinutes);
//            _getValueFunc = getValueFunc;
//        }
//        #endregion

//        #region Members
//        private Func<T> _getValueFunc;
//        private CacheItemPolicy _policy;
//        private ObjectCache _cache;
//        private static object lockingObject = new object();
//        private readonly int _cacheTimeoutMinutes;
//        #endregion

//        #region Properties
//        /// <summary>
//        /// The object value
//        /// </summary>
//        public T Value
//        {
//            get
//            {
//                lock (_cache)
//                {
//                    return GetValue();
//                }
//            }
//            set
//            {
//                _cache[string.Empty] = value;
//            }
//        }
//        #endregion

//        #region Private Methods
//        private T GetValue()
//        {
//            //Try to get the value from the cech
//            var cacheValue = _cache[string.Empty];

//            //if the value is not in the cech
//            if (cacheValue == null)
//            {
//                //get the new value 
//                cacheValue = _getValueFunc.Invoke();

//                //set the new value to the cech
//                _cache.Set(string.Empty,
//                    cacheValue,
//                    _policy);
//                //refresh the cech timeout
//                _policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(_cacheTimeoutMinutes);
//            }
//            return (T)cacheValue;
//        }
//        #endregion
//    }
//}
