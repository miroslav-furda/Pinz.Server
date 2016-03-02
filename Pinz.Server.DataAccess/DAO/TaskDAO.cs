using System.Collections.Generic;
using Ninject;
using System.Linq;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class TaskDAO : ITaskDAO
    {

        private PinzDbContext context;

        [Inject]
        public  TaskDAO(PinzDbContext context)
        {
            this.context = context;
        }

        public Task Create(Task task)
        {
            Task savedTask = context.Tasks.Add(task);
            context.SaveChanges();
            return savedTask;
        }

        public void Delete(Task task)
        {
            context.Entry(task).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Task> ReadByCategory(Category category)
        {
            return context.Tasks.Where(t => t.CategoryId == category.CategoryId).ToList();
        }

        public void Update(Task task)
        {
            context.Entry(task).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
