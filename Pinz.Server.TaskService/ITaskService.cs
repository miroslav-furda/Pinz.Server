using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.Infrastructure;
using System.Collections.Generic;
using System.ServiceModel;

namespace Com.Pinz.Server.TaskService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITaskService" in both code and config file together.
    [ServiceContract(Namespace ="http://pinzonline.com")]
    public interface ITaskService
    {
        [OperationContract]
        [ApplyDataContractResolver]
        List<Task> ReadByCategory(Category category);

        [OperationContract]
        [ApplyDataContractResolver]
        Task Create(Task task);

        [OperationContract]
        [ApplyDataContractResolver]
        void Update(Task task);

        [OperationContract]
        [ApplyDataContractResolver]
        void Delete(Task task);
    }
}
