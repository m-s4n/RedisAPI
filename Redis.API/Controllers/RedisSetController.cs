using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.API.Redis;
using StackExchange.Redis;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisSetController : ControllerBase
    {
        readonly IDatabase _redisDb;

        public RedisSetController(RedisService redisService)
        {
            _redisDb = redisService.GetDatabase(1);
        }

        [HttpGet("[action]")]

        public async Task SetValue(string key, string value)
        {
            
            await _redisDb.SetAddAsync(key, value);
            await _redisDb.KeyExpireAsync(key, DateTime.Now.AddMinutes(5));
        }

        [HttpGet("[action]")]
        public async Task<List<string>> GetValue(string key)
        {
            List<string> namesList = new();
            if(await _redisDb.KeyExistsAsync(key))
            {
                (await _redisDb.SetMembersAsync(key)).ToList().ForEach(item =>
                {
                    namesList.Add(item.ToString());
                });
            }

            return namesList;
        }

        [HttpGet("[action]")]

        public async Task DeleteValue(string key, string value)
        {
            await _redisDb.SetRemoveAsync(key, value);
        }
    }
}
