﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter.Interfaces
{
    public interface IRule
    {
        bool CheckRule(IDatabaseStore database, string userToken, string resource);
    }
}
