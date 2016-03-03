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

        public List<User> ReadAllUsersForCompanyId(Guid companyId)
        {
            return userDAO.ReadAllUsersInCompany(companyId);
        }

        public List<Project> ReadProjectsForCompanyId(Guid companyId)
        {
            return projectDAO.ReadProjectsForCompanyId(companyId);
        }

        public void RemoveUserFromProject(Guid userId, Guid projectId)
        {
            ProjectStaff ps = projectStaffDAO.GetById(userId, projectId);
            projectStaffDAO.Delete(ps);
        }

        public void AddUserToProject(Guid userId, Guid projectId, bool isProjectAdmin)
        {
            ProjectStaff ps = new ProjectStaff();
            ps.ProjectId = projectId;
            ps.UserId = userId;
            ps.IsProjectAdmin = isProjectAdmin;
            projectStaffDAO.Create(ps);
        }

        public Company ReadCompanyById(Guid id)
        {
            return companyDAO.ReadCompanyById(id);
        }

        public Project CreateProject(Project project)
        {
            return projectDAO.Create(project);
        }

        public User CreateUser(User user)
        {
            return userDAO.Create(user);
        }

        public void DeleteProject(Project project)
        {
            projectDAO.Delete(project);
        }

        public void DeleteUser(User user)
        {
            userDAO.Delete(user);
        }

        public void UpdateProject(Project project)
        {
            projectDAO.Update(project);
        }

        public void UpdateUser(User user)
        {
            userDAO.Update(user);
        }

    }
}
