using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

using static System.Net.WebRequestMethods;

namespace PTSL.Ovidhan.Common.Helper
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;

        public CacheService(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }
        public async Task SetAsync(string key, object value) // set value in cache 
        {
            try
            {
                var validity = Convert.ToInt16(_configuration.GetSection("OTP:DurationInMinutes").Value);
                await _cache.SetAsync(key, ConverterService.ObjectToByteArray(value), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(validity) });
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to set cache value");
            }
        }
        public async Task<byte[]> GetAsync(string key) // get value from cache 
        {

            var cachedValue = await _cache.GetAsync(key);
            if (cachedValue == null)
            {
                return new byte[0];
            }
            return cachedValue;
        }
    }
}
