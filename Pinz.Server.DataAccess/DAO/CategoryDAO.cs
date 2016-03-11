using System.Collections.Generic;
using System.Linq;
using Ninject;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess.Db;
using System;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class CategoryDAO : BasicDAO<CategoryDO>, ICategoryDAO
    {
        [Inject]
        public CategoryDAO(PinzDbContext context) : base(context) { }

        public List<CategoryDO> ReadAllByProjectId(Guid projectId)
        {
            return context.Categories.Where(c => c.ProjectId == projectId).ToList();
        }

        protected override DbSet<CategoryDO> GetDbSet()
        {
            return context.Categories;
        }
    }
}