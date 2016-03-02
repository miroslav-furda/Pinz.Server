using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface IUserDAO : IBasicDAO<User>
    {
        User ReadByEmail(string email);
        List<User> ReadAllUsersInCompany(Guid companyId);
        List<User> ReadAllUsersInProject(Guid projectId);
    }
}
