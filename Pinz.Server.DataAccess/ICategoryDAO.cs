﻿using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface ICategoryDAO : IBasicDAO<Category>
    {
        List<Category> ReadAllByProjectId(Guid projectId);
    }
}