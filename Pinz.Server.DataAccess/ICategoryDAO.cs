using Com.Pinz.Server.DataAccess.Model;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface ICategoryDAO
    {
        List<Category> ReadAll();

        void Delete(Category category);

        void Update(Category category);

        Category Create(Category category);
    }
}
