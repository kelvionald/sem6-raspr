using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Valudator
{
    public class RedisStorage : IStorage
    {
        private readonly IDatabase db;
        private string Host = "localhost";
        private int Port = 6379;

        public RedisStorage() {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(Host);
            db = connectionMultiplexer.GetDatabase();
        }

        public string Load(string key)
        {
            return db.StringGet(key);
        }

        public void Store(string key, string value)
        {
            db.StringSet(key, value);
        }

        public bool IsExistsInSet(string setKey, string value)
        {
            return db.SetContains(setKey, value);
        }

        public void StoreToSet(string setKey, string value)
        {
            db.SetAdd(setKey, value);
        }
    }
}