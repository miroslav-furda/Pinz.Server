﻿using System;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.DataAccess
{
    public interface ISubscriptionDAO : IBasicDAO<SubscriptionDO>
    {
        SubscriptionDO ReadById(string subscriptionReference);
    }
}