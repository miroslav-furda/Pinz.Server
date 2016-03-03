using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess;

namespace Com.Pinz.Server.TaskService
{
    public class AuthorisationService : IAuthorisationService
    {
        private IUserDAO userDAO;
        private IProjectStaffDAO projectStaffDAO;

        public AuthorisationService(IUserDAO userDAO, IProjectStaffDAO projectStaffDAO)
        {
            this.userDAO = userDAO;
            this.projectStaffDAO = projectStaffDAO;
        }

        public bool IsUserComapnyAdmin(Guid userId)
        {
            return userDAO.GetById(userId).IsCompanyAdmin;
        }

        public bool IsUserProjectAdmin(Guid userId, Guid projectId)
        {
            return projectStaffDAO.GetById(userId, projectId).IsProjectAdmin;
        }

        public User ReadUserByEmail(string email)
        {
            return userDAO.ReadByEmail(email);
        }
    }
}
