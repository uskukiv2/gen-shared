using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepoDb;
using RepoDb.Interfaces;

namespace alms.cherry.data.process
{
    public class CherryCache : ICache
    {
        private IDictionary _data;

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add<T>(string key, T value, int expiration = 180, bool throwException = true)
        {
            _data ??= new ConcurrentDictionary<string, CacheItem<T>>();

            var tempData = (ConcurrentDictionary<string, CacheItem<T>>) _data;
            if (tempData.ContainsKey(key) && throwException)
            {
                throw new AccessViolationException(nameof(value));
            }

            _data.Add(key, new CacheItem<T>(key, value, expiration));
        }

        public void Add<T>(CacheItem<T> item, bool throwException = true)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(string key)
        {
            return _data.Contains(key);
        }

        public CacheItem<T> Get<T>(string key, bool throwException = true)
        {
            if (_data == null)
            {
                return null;
            }

            var tempData = (ConcurrentDictionary<string, CacheItem<T>>)_data;
            if (!tempData.ContainsKey(key) && throwException)
            {
                throw new ArgumentException(nameof(key));
            }

            return (CacheItem<T>)_data[key];
        }

        public void Remove(string key, bool throwException = true)
        {
            _data?.Remove(key);
        }
    }
}
