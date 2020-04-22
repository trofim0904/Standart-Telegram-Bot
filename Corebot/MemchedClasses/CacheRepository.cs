using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corebot.MemchedClasses
{
    internal interface ICacheRepository
    {
        void Set<T>(string key, T value);
    }

    internal class CacheRepository : ICacheRepository
    {
        private readonly IMemcachedClient memcachedClient;

        public CacheRepository(IMemcachedClient memcachedClient)
        {
            this.memcachedClient = memcachedClient;
        }

        public void Set<T>(string key, T value)
        {
            // Setting cache expiration for an hour
            memcachedClient.Set(key, value, 60 * 60);
        }
    }
}
