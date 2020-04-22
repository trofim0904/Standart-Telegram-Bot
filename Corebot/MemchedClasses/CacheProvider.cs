using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corebot.MemchedClasses
{
    internal interface ICacheProvider
    {
        T GetCache<T>(string key);
    }

    internal class CacheProvider : ICacheProvider
    {
        private readonly IMemcachedClient memcachedClient;

        public CacheProvider(IMemcachedClient memcachedClient)
        {
            this.memcachedClient = memcachedClient;
        }

        public T GetCache<T>(string key)
        {
            return memcachedClient.Get<T>(key);
        }
    }
}
