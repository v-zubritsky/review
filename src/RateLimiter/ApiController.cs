using RateLimiter.Interfaces;
using RateLimiter.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
    public class ApiController
    {
        private readonly IDatabaseStore _database;
        private readonly IResourceLimitService _resourceLimitService;

        public ApiController(IDatabaseStore database, IResourceLimitService resourceLimitService)
        {
            _database = database;
            _resourceLimitService = resourceLimitService;
        }

        public void SomeAction(string userToken)
        {
            var resource = "/api/user/registration";
            var isValid = _resourceLimitService.CheckLimits(new List<IRule>() { new CheckUserRequestsCountPerSecondRule() }, userToken, resource);

            if (!isValid)
            {
                return;
            }

            // Do Some Action!!!
        }
    }
}
