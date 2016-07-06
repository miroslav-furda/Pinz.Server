using System;
using System.Runtime.Serialization;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess;
using System.Security.Permissions;
using Com.Pinz.Server.TaskService.Infrastructure;

namespace Com.Pinz.Server.TaskService
{
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class AuthorisationService : IAuthorisationService
    {
        private IUserDAO userDAO;
        private IProjectStaffDAO projectStaffDAO;

        public AuthorisationService(IUserDAO userDAO, IProjectStaffDAO projectStaffDAO)
        {
            this.userDAO = userDAO;
            this.projectStaffDAO = projectStaffDAO;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public bool IsUserComapnyAdmin(Guid userId)
        {
            return userDAO.GetById(userId).IsCompanyAdmin;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public bool IsUserProjectAdmin(Guid userId, Guid projectId)
        {
            return projectStaffDAO.IsUserAdminInProject(userId, projectId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public UserDO ReadUserByEmail(string email)
        {
            return userDAO.ReadByEmail(email);
        }
    }
}
