using RateLimiter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
    public class ResourceLimitService : IResourceLimitService
    {

        private readonly IDatabaseStore _database;

        public ResourceLimitService(IDatabaseStore database)
        {
            _database = database;
        }

        public bool CheckLimits(IEnumerable<IRule> rules, string userToken, string resource)
            => rules.All(rule => rule.CheckRule(_database, userToken, resource));
    }
}
