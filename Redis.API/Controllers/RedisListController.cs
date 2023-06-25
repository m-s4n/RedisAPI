using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.API.Redis;
using StackExchange.Redis;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisListController : ControllerBase
    {
        readonly IDatabase _redisDb;

        public RedisListController(RedisService redisService)
        {
            _redisDb = redisService.GetDatabase(1);
        }

        [HttpGet("[action]")]
        public async Task SetValue(string key)
        {
            await _redisDb.ListRightPushAsync("liste", key);
            await _redisDb.ListLeftPushAsync("liste", key);
        }

        [HttpGet("[action]")]
        public async Task<List<string>> GetValue()
        {
            List<string> nameList = new();
            // key kontrolu
            if (await _redisDb.KeyExistsAsync("liste"))
            {
                (await _redisDb.ListRangeAsync("liste")).ToList().ForEach(item =>
                {
                    nameList.Add(item.ToString());
                });
            }
            return nameList;
        }

        [HttpGet("[action]")]

        public async Task DeleteValue(string value)
        {
            await _redisDb.ListRemoveAsync("liste", value); 
        }
    }
}
