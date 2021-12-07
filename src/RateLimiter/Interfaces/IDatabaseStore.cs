using System.Collections.Generic;

namespace RateLimiter.Interfaces
{
    public interface IDatabaseStore
    {
        void Add(string userToken, IEnumerable<IUserData> value);

        void Update(
	        string userToken, 
	        IEnumerable<IUserData> value, 
	        IEnumerable<IUserData> comparisonValue);

        IEnumerable<IUserData> Get(string userToken);
    }
}
