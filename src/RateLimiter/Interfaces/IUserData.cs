using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Interfaces
{
    public interface IUserData
    {
        string Resource { get; set; }

        DateTime Date { get; set; }
    }
}
