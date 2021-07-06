using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Interfaces
{
    public interface IResourceLimitService
    {
        bool CheckLimits(IEnumerable<IRule> rules, string userToken, string resource);
    }
}
