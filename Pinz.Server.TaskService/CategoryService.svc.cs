using System.Collections.Generic;
using Ninject;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryDAO categoryDAO;

        [Inject]
        public CategoryService(ICategoryDAO categoryDAO)
        {
            this.categoryDAO = categoryDAO;
        }

        public Category Create(Category category)
        {
            return categoryDAO.Create(category);
        }

        public void Delete(Category category)
        {
            categoryDAO.Delete(category);
        }

        public List<Category> ReadAll()
        {
            return categoryDAO.ReadAll();
        }

        public void Update(Category category)
        {
            categoryDAO.Update(category);
        }
    }
}
