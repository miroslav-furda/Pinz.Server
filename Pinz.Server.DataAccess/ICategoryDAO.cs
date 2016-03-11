using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface ICategoryDAO : IBasicDAO<CategoryDO>
    {
        List<CategoryDO> ReadAllByProjectId(Guid projectId);
    }
}
