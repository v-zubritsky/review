using RateLimiter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Models
{
    public class UserDataModel : IUserData
    {
        public string Resource { get; set; }

        public DateTime Date { get; set; }
    }
}
