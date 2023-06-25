using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.API.Redis;
using StackExchange.Redis;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisHashController : BaseController
    {
        public RedisHashController(RedisService redisService) : base(redisService)
        {
        }

        [HttpGet("[action]")]
        public async Task SetValue(string hashKey)
        {
            HashEntry[] hashEntries = new HashEntry[5]
            {
                new HashEntry("name", "mustafa"),
                new HashEntry("surname", "seymen"),
                new HashEntry("age", 30),
                new HashEntry("id", 1),
                new HashEntry("is", false),

            };

            await _redisDb.HashSetAsync(hashKey,hashEntries);
        }

        [HttpGet("[action]")]
        public async Task<Dictionary<string, object>> GetValue(string hashKey)
        {
            Dictionary<string, object> result = new();

            (await _redisDb.HashGetAllAsync(hashKey)).ToList().ForEach(hashEntry =>
            {
                result.Add(hashEntry.Name.ToString(), hashEntry.Value.ToString());
            });

            return result;
        }
    }
}
