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

        [Inject]
        public TaskService(IProjectDAO projectDAO, ICategoryDAO categoryDAO, ITaskDAO taskDAO)
        {
            this.projectDAO = projectDAO;
            this.categoryDAO = categoryDAO;
            this.taskDAO = taskDAO;
        }

        public List<TaskDO> ReadAllTasksByCategoryId(Guid categoryId)
        {
            return taskDAO.ReadAllByCategoryId(categoryId);
        }

        public List<CategoryDO> ReadAllCategoriesByProjectId(Guid projectId)
        {
            return categoryDAO.ReadAllByProjectId(projectId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public List<ProjectDO> ReadAllProjectsForUserId(Guid userId)
        {
            return projectDAO.ReadAllProjectsForUserId(userId);
        }

        public TaskDO CreateTask(TaskDO task)
        {
            return taskDAO.Create(task);
        }

        public TaskDO UpdateTask(TaskDO task)
        {
            return taskDAO.Update(task);
        }

        public void DeleteTask(TaskDO task)
        {
            taskDAO.Delete(task);
        }

        public CategoryDO CreateCategory(CategoryDO category)
        {
            return categoryDAO.Create(category);
        }

        public CategoryDO UpdateCategory(CategoryDO category)
        {
            return categoryDAO.Update(category);
        }

        public void DeleteCategory(CategoryDO category)
        {
            categoryDAO.Delete(category);
        }
    }
}
