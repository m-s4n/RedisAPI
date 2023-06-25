using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.API.Models;
using Redis.API.Redis;
using StackExchange.Redis;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisStringController : ControllerBase
    {
        readonly IDatabase _redisDb;
        public RedisStringController(RedisService redisService)
        {
            _redisDb = redisService.GetDatabase(1);
        }


        [HttpGet("[action]")]
        public async Task SetValue()
        {
            await _redisDb.StringSetAsync("name:1", "mustafa seymen");
            await _redisDb.StringSetAsync("sayac:1", 100);
            await _redisDb.StringIncrementAsync("sayac:1", 10);
            
        }

        [HttpGet("[action]")]
        public async Task<string> GetValue(string key)
        {
           var value = await _redisDb.StringGetAsync(key);
            if (value.HasValue)
            {
                return value;
            }
            return "sevi";
        }
    }
}
