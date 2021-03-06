﻿using System.Collections.Generic;
using Ninject;
using System.Linq;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess.Db;
using System;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class TaskDAO : BasicDAO<TaskDO>, ITaskDAO
    {
        [Inject]
        public TaskDAO(PinzDbContext context) : base(context){ }

        public List<TaskDO> ReadAllByCategoryId(Guid categoryId)
        {
            return context.Tasks.Where(t => t.CategoryId == categoryId).ToList();
        }

        public List<TaskDO> ReadAllUserTasks(Guid userId)
        {
            return GetDbSet().Where(t => t.UserId == userId).ToList();
        }

        protected override DbSet<TaskDO> GetDbSet()
        {
            return context.Tasks;
        }
    }
}
