using Microsoft.Extensions.Caching.Distributed;

namespace ShoeStoreClothing.Services
{
    public class RedisService
    {
        private readonly IDistributedCache _cache;
        public RedisService(IDistributedCache cache) { 
            _cache = cache;
        }
    }
}
