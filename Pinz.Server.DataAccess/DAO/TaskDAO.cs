using System.Collections.Generic;
using Ninject;
using System.Linq;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess.Db;
using System;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class TaskDAO : BasicDAO<Task>, ITaskDAO
    {
        [Inject]
        public TaskDAO(PinzDbContext context) : base(context){ }

        public List<Task> ReadByCategory(Category category)
        {
            return context.Tasks.Where(t => t.CategoryId == category.CategoryId).ToList();
        }

        protected override DbSet<Task> GetDbSet()
        {
            return context.Tasks;
        }
    }
}
