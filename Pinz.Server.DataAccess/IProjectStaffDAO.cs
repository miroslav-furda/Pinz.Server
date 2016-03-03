using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface IProjectStaffDAO : IBasicDAO<ProjectStaff>
    {
        bool IsUserAdminInProject(Guid userId, Guid projectId);
        ProjectStaff GetById(Guid userId, Guid projectId);
    }
}
