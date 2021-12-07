using RateLimiter.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RateLimiter
{
    public class ResourceLimitService : IResourceLimitService
    {
        private readonly IDatabaseStore _database;

        public ResourceLimitService(IDatabaseStore database)
        {
            _database = database;
        }

        public bool CheckLimits(IEnumerable<IRule> rules, string userToken, string resource) => 
	        rules.All(rule => rule.CheckRule(_database, userToken, resource));
    }
}
