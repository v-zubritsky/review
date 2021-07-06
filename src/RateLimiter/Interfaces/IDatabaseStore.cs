using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Interfaces
{
    public interface IDatabaseStore
    {
        void Add(string userToken, IEnumerable<IUserData> value);

        void Update(string userToken, IEnumerable<IUserData> value, IEnumerable<IUserData> companisonValue);

        IEnumerable<IUserData> Get(string userToken);
    }
}
