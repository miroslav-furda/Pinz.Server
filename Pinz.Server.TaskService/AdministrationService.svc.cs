using System;
using System.Collections.Generic;
using System.Linq;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess;
using Ninject;
using System.Security.Permissions;

namespace Com.Pinz.Server.TaskService
{
    public class AdministrationService : IAdministrationService
    {
        private IProjectDAO projectDAO;
        private IUserDAO userDAO;
        private ICompanyDAO companyDAO;
        private IProjectStaffDAO projectStaffDAO;

        [Inject]
        public AdministrationService(IProjectDAO projectDAO, IUserDAO userDAO, ICompanyDAO companyDAO, IProjectStaffDAO projectStaffDAO)
        {
            this.projectDAO = projectDAO;
            this.userDAO = userDAO;
            this.companyDAO = companyDAO;
            this.projectStaffDAO = projectStaffDAO;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public List<UserDO> ReadAllUsersForCompanyId(Guid companyId)
        {
            return userDAO.ReadAllUsersInCompany(companyId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public List<ProjectDO> ReadProjectsForCompanyId(Guid companyId)
        {
            return projectDAO.ReadProjectsForCompanyId(companyId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public void RemoveUserFromProject(Guid userId, Guid projectId)
        {
            ProjectStaffDO ps = projectStaffDAO.GetById(userId, projectId);
            projectStaffDAO.Delete(ps);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public void AddUserToProject(Guid userId, Guid projectId, bool isProjectAdmin)
        {
            ProjectStaffDO ps = new ProjectStaffDO();
            ps.ProjectId = projectId;
            ps.UserId = userId;
            ps.IsProjectAdmin = isProjectAdmin;
            projectStaffDAO.Create(ps);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public CompanyDO ReadCompanyById(Guid id)
        {
            return companyDAO.ReadCompanyById(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public ProjectDO CreateProject(ProjectDO project)
        {
            return projectDAO.Create(project);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public UserDO CreateUser(UserDO user)
        {
            user.Password = "test";
            return userDAO.Create(user);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public void DeleteProject(ProjectDO project)
        {
            projectDAO.Delete(project);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public void DeleteUser(UserDO user)
        {
            userDAO.Delete(user);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public ProjectDO UpdateProject(ProjectDO project)
        {
            return projectDAO.Update(project);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public UserDO UpdateUser(UserDO user)
        {
            UserDO originlUser = userDAO.ReadByEmail(user.EMail);
            originlUser.FamilyName = user.FamilyName;
            originlUser.FirstName = user.FirstName;
            originlUser.IsCompanyAdmin = user.IsCompanyAdmin;
            return userDAO.Update(originlUser);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public bool ChangeUserPassword(Guid userId, string oldPassword, string newPassword, string newPassword2)
        {
            UserDO originalUser = userDAO.GetById(userId);
            if( !String.IsNullOrEmpty(oldPassword) && !String.IsNullOrEmpty(newPassword) && !String.IsNullOrEmpty(newPassword2)
                && newPassword.Length >= 6 && oldPassword.Equals(originalUser.Password) && newPassword.Equals(newPassword2))
            {
                originalUser.Password = newPassword;
                userDAO.Update(originalUser);
                return true;
            }
            return false;
        }
    }
}
