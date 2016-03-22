using System.Collections.Generic;
using Ninject;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Security.Permissions;

namespace Com.Pinz.Server.TaskService
{
    public class TaskService : ITaskService
    {
        private ITaskDAO taskDAO;
        private ICategoryDAO categoryDAO;
        private IProjectDAO projectDAO;
        private IUserDAO userDAO;

        [Inject]
        public TaskService(IProjectDAO projectDAO, ICategoryDAO categoryDAO, ITaskDAO taskDAO, IUserDAO userDAO)
        {
            this.projectDAO = projectDAO;
            this.categoryDAO = categoryDAO;
            this.taskDAO = taskDAO;
            this.userDAO = userDAO;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public List<TaskDO> ReadAllTasksByCategoryId(Guid categoryId)
        {
            return taskDAO.ReadAllByCategoryId(categoryId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public List<CategoryDO> ReadAllCategoriesByProjectId(Guid projectId)
        {
            return categoryDAO.ReadAllByProjectId(projectId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public List<ProjectDO> ReadAllProjectsForUserEmail(string email)
        {
            UserDO user = userDAO.ReadByEmail(email);
            return projectDAO.ReadAllProjectsForUserId(user.UserId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public TaskDO CreateTask(TaskDO task, string userEmail)
        {
            UserDO user = userDAO.ReadByEmail(userEmail);
            task.UserId = user.UserId;
            return taskDAO.Create(task);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public TaskDO UpdateTask(TaskDO task)
        {
            return taskDAO.Update(task);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public void DeleteTask(TaskDO task)
        {
            taskDAO.Delete(task);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public CategoryDO CreateCategory(CategoryDO category)
        {
            return categoryDAO.Create(category);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public CategoryDO UpdateCategory(CategoryDO category)
        {
            return categoryDAO.Update(category);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "USER")]
        public void DeleteCategory(CategoryDO category)
        {
            categoryDAO.Delete(category);
        }
    }
}
