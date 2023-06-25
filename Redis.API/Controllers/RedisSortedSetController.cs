using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.API.Redis;
using StackExchange.Redis;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisSortedSetController : ControllerBase
    {
        readonly IDatabase _redisDb;
        public RedisSortedSetController(RedisService redisService)
        {
            _redisDb = redisService.GetDatabase(1);
        }

        [HttpGet("[action]")]
        public async Task SetValue(string key, string value, double score)
        {
            await _redisDb.SortedSetAddAsync(key, value, score);
        }

        [HttpGet("[action]")]
        public async Task<List<string>> GetValue(string key)
        {
            List<string> namesList = new();
            if (await _redisDb.KeyExistsAsync(key))
            {
                (await _redisDb.SortedSetRangeByScoreAsync(key)).ToList().ForEach(item =>
                {
                    namesList.Add(item.ToString());
                });
            }

            return namesList;
        }

        [HttpGet("[action]")]
        public async Task<List<object>> GetValueWithScore(string key)
        {
            List<object> valuesList = new();
            if(await _redisDb.KeyExistsAsync(key))
            {
                (await _redisDb.SortedSetRangeByScoreWithScoresAsync(key)).ToList().ForEach(item =>
                {
                    valuesList.Add(new { name = item.Element.ToString(), puan = item.Score});
                });
            }

            return valuesList;
        }
        
    }
}
