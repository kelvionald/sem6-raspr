using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Valudator
{
    public class RedisStorage : IStorage
    {
        private readonly ILogger<RedisStorage> _logger;
        private readonly IDatabase db;
        private string Host = "localhost";
        private int Port = 6379;

        public RedisStorage(ILogger<RedisStorage> logger) {
            _logger = logger;
            
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(Host);
            db = connectionMultiplexer.GetDatabase();
        }

        public string Load(string key)
        {
            return db.StringGet(key);
        }

        public bool IsExistsByValue(string value)
        {
            var keys = ConnectionMultiplexer.Connect(Host).GetServer(Host + ":" + Port).Keys(pattern: "*TEXT-*");

            foreach (var key in keys) {
                string _value = Load(key);
                if (_value == value) {
                    return true;
                }
            }
            return false;
        }

        public void Store(string key, string value)
        {
            db.StringSet(key, value);
        }
    }
}