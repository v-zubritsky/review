namespace RateLimiter.Interfaces
{
    public interface IRule
    {
        bool CheckRule(IDatabaseStore database, string userToken, string resource);
    }
}
