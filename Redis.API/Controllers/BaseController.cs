using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.API.Redis;
using StackExchange.Redis;

namespace Redis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IDatabase _redisDb;

        public BaseController(RedisService redisService) 
        {
            _redisDb = redisService.GetDatabase(1);
        }
    }
}
