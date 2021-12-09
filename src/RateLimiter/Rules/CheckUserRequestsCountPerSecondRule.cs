using RateLimiter.Interfaces;
using RateLimiter.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RateLimiter.Rules
{
    public class CheckUserRequestsCountPerSecondRule : IRule
    {
        const int TryCount = 5;
        const int TimeSeconds = 60;

        public bool CheckRule(IDatabaseStore database, string userToken, string resource)
        {
            if (database == null || 
                string.IsNullOrEmpty(userToken) || 
                string.IsNullOrEmpty(resource))
            {
                return true;
            }

            var now = DateTime.Now;
            var storeRecords = database.Get(userToken);
            var newRecord = new List<UserDataModel>
            {
	            new UserDataModel
	            {
		            Resource = resource,
		            Date = now
	            }
            };

            if (storeRecords == null)
            {
                database.Add(userToken, newRecord);
                return true;
            }

            var tryCount = storeRecords.Count(
                x => x.Date >= now.AddSeconds(-TimeSeconds) && x.Resource == resource);
            var newRecords = storeRecords.Concat(newRecord);
            database.Update(userToken, newRecords, storeRecords);

            return tryCount < TryCount;
        }
    }
}
