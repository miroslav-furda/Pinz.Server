using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.Infrastructure;
using Com.Pinz.Server.TaskService.InviteUser;
using Ninject;

namespace Com.Pinz.Server.TaskService
{
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class AdministrationService : IAdministrationService
    {
        private readonly ICompanyDAO _companyDao;
        private readonly IProjectDAO _projectDao;
        private readonly IProjectStaffDAO _projectStaffDao;
        private readonly ITaskDAO _taskDao;
        private readonly IUserDAO _userDao;

        [Inject]
        public AdministrationService(IProjectDAO projectDao, IUserDAO userDao, ICompanyDAO companyDao,
            IProjectStaffDAO projectStaffDao, ITaskDAO taskDao)
        {
            this._projectDao = projectDao;
            this._userDao = userDao;
            this._companyDao = companyDao;
            this._projectStaffDao = projectStaffDao;
            this._taskDao = taskDao;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public List<UserDO> ReadAllUsersForCompanyId(Guid companyId)
        {
            return _userDao.ReadAllUsersInCompany(companyId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public List<ProjectDO> ReadProjectsForCompanyId(Guid companyId)
        {
            return _projectDao.ReadProjectsForCompanyId(companyId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public void RemoveUserFromProject(Guid userId, Guid projectId)
        {
            var ps = _projectStaffDao.GetById(userId, projectId);
            _projectStaffDao.Delete(ps);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public void AddUserToProject(Guid userId, Guid projectId, bool isProjectAdmin)
        {
            var ps = new ProjectStaffDO();
            ps.ProjectId = projectId;
            ps.UserId = userId;
            ps.IsProjectAdmin = isProjectAdmin;
            _projectStaffDao.Create(ps);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public CompanyDO ReadCompanyById(Guid id)
        {
            return _companyDao.ReadCompanyById(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public ProjectDO CreateProject(ProjectDO project)
        {
            if (CanCreateProject(project))
                return _projectDao.Create(project);
            throw new InvalidOperationException("Failed to create new Project. Too many projects for this subscription");
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public UserDO CreateUser(UserDO user)
        {
            user.Password = "test";
            return _userDao.Create(user);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public void DeleteProject(ProjectDO project)
        {
            _projectDao.Delete(project);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public void DeleteUser(UserDO user)
        {
            _projectStaffDao.DeleteAllStaffingForUser(user.UserId);

            var usersTasks = _taskDao.ReadAllUserTasks(user.UserId);
            foreach (var task in usersTasks)
            {
                task.UserId = null;
                task.User = null;
                _taskDao.Update(task);
            }

            _userDao.Delete(user);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public ProjectDO UpdateProject(ProjectDO project)
        {
            return _projectDao.Update(project);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public UserDO UpdateUser(UserDO user)
        {
            var originlUser = _userDao.ReadByEmail(user.EMail);
            originlUser.FamilyName = user.FamilyName;
            originlUser.FirstName = user.FirstName;
            originlUser.IsCompanyAdmin = user.IsCompanyAdmin;
            return _userDao.Update(originlUser);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public bool ChangeUserPassword(Guid userId, string oldPassword, string newPassword, string newPassword2)
        {
            var originalUser = _userDao.GetById(userId);
            if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword) &&
                !string.IsNullOrEmpty(newPassword2)
                && (newPassword.Length >= 6) && oldPassword.Equals(originalUser.Password) &&
                newPassword.Equals(newPassword2))
            {
                originalUser.Password = newPassword;
                _userDao.Update(originalUser);
                return true;
            }
            return false;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public List<ProjectDO> ReadAdminProjectsForUser(Guid userId)
        {
            List<ProjectDO> projects;
            var user = _userDao.GetById(userId);
            if (user.IsCompanyAdmin)
                projects = _projectDao.ReadProjectsForCompanyId(user.CompanyId);
            else
                projects = _projectDao.ReadAdminProjectsForUserId(userId);
            return projects;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public List<UserDO> ReadAllUsersByProject(Guid projectId)
        {
            return _userDao.ReadAllUsersInProject(projectId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public List<ProjectUserDO> ReadAllProjectUsersInProject(Guid projectId)
        {
            return _projectStaffDao.ReadAllProjectUsersInProject(projectId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public void SetProjectAdminFlag(Guid userId, Guid projectId, bool isProjectAdmin)
        {
            var ps = _projectStaffDao.GetById(userId, projectId);
            ps.IsProjectAdmin = isProjectAdmin;
            _projectStaffDao.Update(ps);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PROJECT_ADMIN")]
        public UserDO InviteNewUser(string newUserEmail, Guid projectId, Guid invitingUserId)
        {
            var project = _projectDao.GetById(projectId);
            var invitingUser = _userDao.GetById(invitingUserId);

            var generatedPassword = RandomPassword.Generate();
            var user = new UserDO
            {
                EMail = newUserEmail,
                IsCompanyAdmin = false,
                IsPinzSuperAdmin = false,
                CompanyId = invitingUser.CompanyId,
                Password = generatedPassword
            };
            user = _userDao.Create(user);
            AddUserToProject(user.UserId, projectId, false);

            InvitationEmailSender.SendProjectInvitation(newUserEmail, invitingUser, project, generatedPassword);

            return user;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public bool CanCreateProject(ProjectDO project)
        {
            var projects = _projectDao.ReadProjectsForCompanyId(project.CompanyId);
            var subscription = _companyDao.ReadSubscriptionByCompanyId(project.CompanyId);
            return subscription.Quantity > projects.Count;
        }
    }
}