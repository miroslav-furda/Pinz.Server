using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.Infrastructure;
using System.Collections.Generic;
using System.ServiceModel;

namespace Com.Pinz.Server.TaskService
{
    [ServiceContract(Namespace = "http://pinzonline.com")]
    public interface ICategoryService
    {
        [OperationContract]
        [ApplyDataContractResolver]
        List<Category> ReadAll();

        [OperationContract]
        [ApplyDataContractResolver]
        Category Create(Category category);

        [OperationContract]
        [ApplyDataContractResolver]
        void Update(Category category);

        [OperationContract]
        [ApplyDataContractResolver]
        void Delete(Category category);
    }
}
