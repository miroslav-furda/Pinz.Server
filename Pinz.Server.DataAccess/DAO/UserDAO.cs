using System;
using System.Collections.Generic;
using System.Linq;
using Com.Pinz.Server.DataAccess.Model;
using Ninject;
using Com.Pinz.Server.DataAccess.Db;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class UserDAO : BasicDAO<User>, IUserDAO
    {
        [Inject]
        public UserDAO(PinzDbContext context) : base(context) { }

        public List<User> ReadAllUsersInCompany(Guid companyId)
        {
            return GetDbSet().Where(u => u.CompanyId == companyId).ToList();
        }

        public List<User> ReadAllUsersInProject(Guid projectId)
        {
            return GetDbSet().Where(u => u.ProjectStaff.Any(ps => ps.ProjectId == projectId)).ToList();
        }

        public User ReadByEmail(string email)
        {
            return GetDbSet().Where(u => u.EMail == email).SingleOrDefault();
        }

        protected override DbSet<User> GetDbSet()
        {
            return context.Users;
        }
    }
}
