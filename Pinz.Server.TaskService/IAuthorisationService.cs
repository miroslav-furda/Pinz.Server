using Com.Pinz.Server.DataAccess.Model;
using System;
using System.ServiceModel;

namespace Com.Pinz.Server.TaskService
{
    [ServiceContract(Namespace = "http://pinzonline.com/services")]
    public interface IAuthorisationService
    {
        [OperationContract]
        bool IsUserProjectAdmin(Guid userId, Guid projectId);

        [OperationContract]
        bool IsUserComapnyAdmin(Guid userId);

        [OperationContract]
        User ReadUserByEmail(string email);
    }
}
