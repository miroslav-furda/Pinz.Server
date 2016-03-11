using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface ITaskDAO : IBasicDAO<TaskDO>
    {
        List<TaskDO> ReadAllByCategoryId(Guid categoryId);
    }
}
