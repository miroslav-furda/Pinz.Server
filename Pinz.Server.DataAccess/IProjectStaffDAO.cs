using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface IProjectStaffDAO : IBasicDAO<ProjectStaffDO>
    {
        bool IsUserAdminInProject(Guid userId, Guid projectId);
        ProjectStaffDO GetById(Guid userId, Guid projectId);
        List<ProjectUserDO> ReadAllProjectUsersInProject(Guid projectId);
    }
}
