using System;
using System.Linq;
using System.Runtime.Caching;

namespace Caching
{
    /// <summary>
    ///  Represents a collection of keys and values(Dictionary) with caching.
    /// </summary>
    public static class Cache
    {
        #region Ctor
        public static void Initialize()
        {
            _cache = new MemoryCache(Guid.NewGuid().ToString());
            _policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30/*cacheTimeoutMinutes*/) };
        }
        #endregion

        #region Members
        private static ObjectCache _cache;
        private static CacheItemPolicy _policy;
        private static int _cacheTimeoutMinutes;
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the number of key/value pairs contained in the System.Runtime.Caching.ObjectCache
        /// </summary>
        public static int Count
        {
            get
            {
                return _cache.Count();
            }
        }

        /// <summary>
        /// Add new item to the dictionary.
        /// </summary>
        /// <param name="key">The key in the dictionary</param>
        /// <param name="value">The value in the dictionary</param>
        /// <param name="absoluteExpiration"></param>
        private static void Add(string key, object value, DateTime absoluteExpiration)
        {
            _cache.Set(key, value, absoluteExpiration);
        }

        /// <summary>
        /// Add new item to the dictionary.
        /// </summary>
        /// <param name="key">The key in the dictionary</param>
        /// <param name="value">The value in the dictionary</param>
        /// <param name="absoluteExpiration"></param>
        private static void Add(string key, object value)
        {
            _cache.Set(key, value, _policy);
        }

        /// <summary>
        /// Remove item from the dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            return (_cache.Remove(key) != null);
        }

        /// <summary>
        /// Check if the key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsKey(string key)
        {
            var cacheValue = _cache[key];
            return cacheValue != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet<T>(string key, out T value)
        {
            lock (_cache)
            {
                //Try to get the value from the cech
                var cacheValue = _cache[key];
                //if the value is not in the cache
                if (cacheValue == null)
                {
                    //value = default(T);
                    value = default(T);
                    return false;
                }
                value = (T) cacheValue;
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(string key, T value)
        {
            lock (_cache)
            {
                //Try to get the value from the cech
                var cacheValue = _cache[key];

                //if the value is not in the cache
                if (cacheValue == null)
                    //set the new value to the cech
                    Add(key, value);
                else
                    throw new Exception(string.Format("key {0} alread in cache", key));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        public static void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            lock (_cache)
            {
                //Try to get the value from the cech
                var cacheValue = _cache[key];

                //if the value is not in the cache
                if (cacheValue == null)
                    //set the new value to the cech
                    Add(key, value, absoluteExpiration);
                else
                    throw new Exception(string.Format("key {0} alread in cache", key));
            }
        }
        #endregion
    }
}
