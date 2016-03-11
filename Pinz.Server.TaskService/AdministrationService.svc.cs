using System;
using System.Collections.Generic;
using System.Linq;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess;
using Ninject;

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

        public List<UserDO> ReadAllUsersForCompanyId(Guid companyId)
        {
            return userDAO.ReadAllUsersInCompany(companyId);
        }

        public List<ProjectDO> ReadProjectsForCompanyId(Guid companyId)
        {
            return projectDAO.ReadProjectsForCompanyId(companyId);
        }

        public void RemoveUserFromProject(Guid userId, Guid projectId)
        {
            ProjectStaffDO ps = projectStaffDAO.GetById(userId, projectId);
            projectStaffDAO.Delete(ps);
        }

        public void AddUserToProject(Guid userId, Guid projectId, bool isProjectAdmin)
        {
            ProjectStaffDO ps = new ProjectStaffDO();
            ps.ProjectId = projectId;
            ps.UserId = userId;
            ps.IsProjectAdmin = isProjectAdmin;
            projectStaffDAO.Create(ps);
        }

        public CompanyDO ReadCompanyById(Guid id)
        {
            return companyDAO.ReadCompanyById(id);
        }

        public ProjectDO CreateProject(ProjectDO project)
        {
            return projectDAO.Create(project);
        }

        public UserDO CreateUser(UserDO user)
        {
            return userDAO.Create(user);
        }

        public void DeleteProject(ProjectDO project)
        {
            projectDAO.Delete(project);
        }

        public void DeleteUser(UserDO user)
        {
            userDAO.Delete(user);
        }

        public void UpdateProject(ProjectDO project)
        {
            projectDAO.Update(project);
        }

        public void UpdateUser(UserDO user)
        {
            userDAO.Update(user);
        }

    }
}
