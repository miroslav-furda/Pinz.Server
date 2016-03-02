using System.Collections.Generic;
using Ninject;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TaskService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TaskService.svc or TaskService.svc.cs at the Solution Explorer and start debugging.
    public class TaskService : ITaskService
    {
        private ITaskDAO taskDAO;

        [Inject]
        public TaskService(ITaskDAO taskDAO)
        {
            this.taskDAO = taskDAO;
        }

        public void Delete(Task task)
        {
            taskDAO.Delete(task);
        }

        public List<Task> ReadByCategory(Category category)
        {
            return taskDAO.ReadAllByCategoryId(category.CategoryId);
        }

        public void Update(Task task)
        {
            taskDAO.Update(task);
        }

        public Task Create(Task task)
        {
            return taskDAO.Create(task);
        }
    }
}
