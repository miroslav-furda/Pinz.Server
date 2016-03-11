using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface IUserDAO : IBasicDAO<UserDO>
    {
        UserDO ReadByEmail(string email);
        List<UserDO> ReadAllUsersInCompany(Guid companyId);
        List<UserDO> ReadAllUsersInProject(Guid projectId);
        UserDO GetById(Guid userId);
    }
}
