using Com.Pinz.Server.DataAccess.Model;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess
{
    public interface ITaskDAO
    {
        void Delete(Task task);

        List<Task> ReadByCategory(Category category);

        void Update(Task task);

        Task Create(Task task);
    }
}
