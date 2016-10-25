using System;
using System.Collections.Generic;
using System.Linq;
using Com.Pinz.Server.DataAccess.Model;
using Ninject;
using Com.Pinz.Server.DataAccess.Db;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class UserDAO : BasicDAO<UserDO>, IUserDAO
    {
        [Inject]
        public UserDAO(PinzDbContext context) : base(context) { }

        public UserDO GetById(Guid userId)
        {
            return GetDbSet( ).Single(u => u.UserId == userId);
        }

        public List<UserDO> ReadAllUsersInCompany(Guid companyId)
        {
            return GetDbSet().Where(u => u.CompanyId == companyId).ToList();
        }

        public List<UserDO> ReadAllUsersInProject(Guid projectId)
        {
            return GetDbSet().Where(u => u.ProjectStaff.Any(ps => ps.ProjectId == projectId)).ToList();
        }

        public UserDO ReadByEmail(string email)
        {
            return GetDbSet().SingleOrDefault(u => u.EMail == email);
        }

        protected override DbSet<UserDO> GetDbSet()
        {
            return context.Users;
        }
    }
}
