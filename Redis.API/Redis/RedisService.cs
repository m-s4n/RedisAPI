using StackExchange.Redis;

namespace Redis.API.Redis
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(string redisConfig)
        {

            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfig);
        }

        public IDatabase GetDatabase(int db)
        {
            return _connectionMultiplexer.GetDatabase(db);
        }
        
        



    }
}
