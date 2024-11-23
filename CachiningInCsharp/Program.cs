using System.Runtime.Caching;

namespace CachingInCsharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cacheKey = "myCache";
            var cachedList = GetFromCache(cacheKey);

            CheckCache(cacheKey, ref cachedList);
            CheckCache(cacheKey, ref cachedList);
        }

        static void CheckCache(string key, ref List<string> cachedList)
        {
            if (cachedList == null)
            {
                List<string> list = new List<string>() { "apple", "banana", "cherry" };
                AddToCache(key, list, TimeSpan.FromMinutes(10));
                Console.WriteLine("List is added to the cache");
                cachedList = GetFromCache(key);
            }
            else
            {
                Console.WriteLine("List is retrieved from the cache.");

                foreach (var item in cachedList)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static List<string> GetFromCache(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            return cache[key] as List<string>;
        }

        static void AddToCache(string key, List<string> list, TimeSpan expirationTime)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.Add(expirationTime)
            };

            cache.Set(key, list, policy);
        }
    }
}
