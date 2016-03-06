﻿using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface ITaskDAO : IBasicDAO<Task>
    {
        List<Task> ReadAllByCategoryId(Guid categoryId);
    }
}