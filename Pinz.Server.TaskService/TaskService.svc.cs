using System.Collections.Generic;
using Ninject;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using System;

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

        public List<Task> ReadAllTasksByCategoryId(Guid categoryId)
        {
            return taskDAO.ReadAllByCategoryId(categoryId);
        }

        public List<Category> ReadAllCategoriesByProjectId(Guid projectId)
        {
            return categoryDAO.ReadAllByProjectId(projectId);
        }

        public List<Project> ReadAllProjectsForUserId(Guid userId)
        {
            return projectDAO.ReadAllProjectsForUserId(userId);
        }

        public Task CreateTask(Task task)
        {
            return taskDAO.Create(task);
        }

        public void UpdateTask(Task task)
        {
            taskDAO.Update(task);
        }

        public void DeleteTask(Task task)
        {
            taskDAO.Delete(task);
        }

        public Category CreateCategory(Category category)
        {
            return categoryDAO.Create(category);
        }

        public void UpdateCategory(Category category)
        {
            categoryDAO.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            categoryDAO.Delete(category);
        }
    }
}
