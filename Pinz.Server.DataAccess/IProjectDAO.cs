using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface IProjectDAO : IBasicDAO<ProjectDO>
    {
        List<ProjectDO> ReadProjectsForCompanyId(Guid companyId);
        List<ProjectDO> ReadAllProjectsForUserId(Guid userId);
        List<ProjectDO> ReadAdminProjectsForUserId(Guid userId);
        ProjectDO GetById(Guid projectId);
    }
}
