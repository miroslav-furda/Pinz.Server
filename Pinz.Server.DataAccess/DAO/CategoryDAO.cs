using System.Collections.Generic;
using System.Linq;
using Ninject;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class CategoryDAO : ICategoryDAO
    {
        private PinzDbContext context;

        [Inject]
        public CategoryDAO(PinzDbContext context)
        {
            this.context = context;
        }

        public Category Create(Category category)
        {
            Category savedCategory = context.Categories.Add(category);
            context.SaveChanges();
            return savedCategory;
        }

        public void Delete(Category category)
        {
            context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Category> ReadAll()
        {
            return context.Categories.ToList();
        }

        public void Update(Category category)
        {
            context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
