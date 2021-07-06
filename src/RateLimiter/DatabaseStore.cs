using RateLimiter.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RateLimiter
{
    public class DatabaseStore : IDatabaseStore
    {
        private static ConcurrentDictionary<string, IEnumerable<IUserData>> store = new ConcurrentDictionary<string, IEnumerable<IUserData>>();

        public void Update(string userToken, IEnumerable<IUserData> value, IEnumerable<IUserData> companisonValue)
        {
            store.TryUpdate(userToken, value, companisonValue);
        }

        public void Add(string userToken, IEnumerable<IUserData> value)
        {
            store.TryAdd(userToken, value);
        }

        public IEnumerable<IUserData> Get(string userToken)
        {
            store.TryGetValue(userToken, out IEnumerable<IUserData> value);
            return value;
        }
    }
}