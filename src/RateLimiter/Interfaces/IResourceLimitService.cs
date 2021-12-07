using System.Collections.Generic;

namespace RateLimiter.Interfaces
{
    public interface IResourceLimitService
    {
        bool CheckLimits(IEnumerable<IRule> rules, string userToken, string resource);
    }
}
