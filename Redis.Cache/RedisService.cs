using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Cache
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string redisConfig)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfig);
        }

        public IDatabase GetDatabase(int db = 1) 
        {
            return _connectionMultiplexer.GetDatabase(db);
        }
    }
}
