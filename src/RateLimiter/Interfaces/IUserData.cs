using System;

namespace RateLimiter.Interfaces
{
    public interface IUserData
    {
        string Resource { get; set; }
        DateTime Date { get; set; }
    }
}
