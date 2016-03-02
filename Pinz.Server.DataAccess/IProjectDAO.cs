using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface IProjectDAO : IBasicDAO<Project>
    {
        List<Project> ReadProjectsForCompanyId(Guid companyId);
        List<Project> ReadAllProjectsForUserId(Guid userId);
    }
}
