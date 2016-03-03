using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.Infrastructure;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Com.Pinz.Server.TaskService
{
    [ServiceContract(Namespace = "http://pinzonline.com/services")]
    public interface IAdministrationService
    {
        [OperationContract]
        [ApplyDataContractResolver]
        List<Project> ReadProjectsForCompanyId(Guid companyId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<User> ReadAllUsersForCompanyId(Guid companyId);

        [OperationContract]
        [ApplyDataContractResolver]
        Company ReadCompanyById(Guid id);

        [OperationContract]
        [ApplyDataContractResolver]
        void AddUserToProject(Guid userId, Guid projectId, bool isProjectAdmin);

        [OperationContract]
        [ApplyDataContractResolver]
        void RemoveUserFromProject(Guid userId, Guid projectId);

        [OperationContract]
        [ApplyDataContractResolver]
        Project CreateProject(Project project);

        [OperationContract]
        [ApplyDataContractResolver]
        void UpdateProject(Project project);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteProject(Project project);

        [OperationContract]
        [ApplyDataContractResolver]
        User CreateUser(User user);

        [OperationContract]
        [ApplyDataContractResolver]
        void UpdateUser(User user);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteUser(User user);
    }
}
