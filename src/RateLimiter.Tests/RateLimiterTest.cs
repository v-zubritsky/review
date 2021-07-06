using RateLimiter.Interfaces;
using RateLimiter.Models;
using RateLimiter.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RateLimiter.Tests
{
    public class RateLimiterTest
    {
        [Theory]
        [InlineData("", "/api/user/registration")]
        [InlineData("user-token", "")]
        public void IsValidWhenAnyParameterIsNullOrEmptyWithoutStore(string userToken, string resource)
        {
            // Arrange
            var store = new DatabaseStore();

            // Act
            var checkUserRequestsCountPerSecondRule = new CheckUserRequestsCountPerSecondRule();
            var result = checkUserRequestsCountPerSecondRule.CheckRule(store, userToken, resource);

            // Assert
            Assert.NotNull(store);
            Assert.NotNull(checkUserRequestsCountPerSecondRule);
            Assert.True(result);
        }

        [Theory]
        [InlineData("user-token", "/api/user/registration")]
        [InlineData("user-token", "/api/products")]
        public void IsValidWhenStoreIsNull(string userToken, string resource)
        {
            // Arrange

            // Act
            var checkUserRequestsCountPerSecondRule = new CheckUserRequestsCountPerSecondRule();
            var result = checkUserRequestsCountPerSecondRule.CheckRule(null, userToken, resource);

            // Assert
            Assert.NotNull(checkUserRequestsCountPerSecondRule);
            Assert.True(result);
        }

        [Theory]
        [InlineData("user-token", "/api/user/registration")]
        [InlineData("user-token", "/api/products")]
        public void ResourceAllowsFiveRequestsPerMinute(string userToken, string resource)
        {
            // Arrange
            var store = new DatabaseStore();
            store.Add(userToken, new List<UserDataModel>()
            {
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-15) },
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-23) },
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-45) },
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-59) }
            });

            // Act
            var checkUserRequestsCountPerSecondRule = new CheckUserRequestsCountPerSecondRule();
            var result = checkUserRequestsCountPerSecondRule.CheckRule(store, userToken, resource);

            // Assert
            Assert.NotNull(store);
            Assert.NotNull(checkUserRequestsCountPerSecondRule);
            Assert.True(result);
        }

        [Theory]
        [InlineData("user-token", "/api/user/registration")]
        public void ResourceDoesNotAllowsFiveRequestsPerMinute(string userToken, string resource)
        {
            // Arrange
            var store = new DatabaseStore();
            store.Add(userToken, new List<UserDataModel>()
            {
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-15) },
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-23) },
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-45) },
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-20) },
                new UserDataModel { Resource = resource, Date = DateTime.Now.AddSeconds(-42) }
            });

            // Act
            var checkUserRequestsCountPerSecondRule = new CheckUserRequestsCountPerSecondRule();
            var result = checkUserRequestsCountPerSecondRule.CheckRule(store, userToken, resource);

            // Assert
            Assert.NotNull(store);
            Assert.NotNull(checkUserRequestsCountPerSecondRule);
            Assert.False(result);
        }

        [Theory]
        [InlineData("user-token-jwt", "/api/user/registration")]
        public void ResourceAddsInDatabaseWhenUserTokenDoesNotExists(string userToken, string resource)
        {
            // Arrange
            var store = new DatabaseStore();

            // Act
            var isNull = store.Get(userToken);
            var checkUserRequestsCountPerSecondRule = new CheckUserRequestsCountPerSecondRule();

            var firstTimeCheckResult = checkUserRequestsCountPerSecondRule.CheckRule(store, userToken, resource);
            var firstTimeCheckResultRules = store.Get(userToken);

            var secondTimeCheckResult = checkUserRequestsCountPerSecondRule.CheckRule(store, userToken, resource);
            var secondTimeCheckResultRules = store.Get(userToken);

            // Assert
            Assert.NotNull(store);
            Assert.NotNull(checkUserRequestsCountPerSecondRule);

            Assert.Null(isNull);
            Assert.NotNull(firstTimeCheckResultRules);

            Assert.Equal(1, Enumerable.Count(firstTimeCheckResultRules));
            Assert.True(firstTimeCheckResult);

            Assert.Equal(2, Enumerable.Count(secondTimeCheckResultRules));
            Assert.True(secondTimeCheckResult);
        }

        [Theory]
        [InlineData("user-token", "/api/user/transactions")]
        [InlineData("user-token", "/api/products")]
        public void CheckLimitsAllows(string userToken, string resource)
        {
            // Arrange
            var store = new DatabaseStore();
            var resourceLimitService = new ResourceLimitService(store);

            // Act
            var result = resourceLimitService.CheckLimits(new List<IRule>() { new CheckUserRequestsCountPerSecondRule() }, userToken, resource);

            // Assert
            Assert.NotNull(store);
            Assert.NotNull(resourceLimitService);
            Assert.True(result);
        }
    }
}
