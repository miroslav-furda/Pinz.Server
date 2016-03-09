using System;
using System.Runtime.Serialization;
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
            return projectStaffDAO.IsUserAdminInProject(userId, projectId);
        }

        public User ReadUserByEmail(string email)
        {
            return userDAO.ReadByEmail(email);
        }
    }
}
