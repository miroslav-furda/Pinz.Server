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
        UserDO InviteNewUser(string newUserEmail, Guid projectId, Guid invitingUserId);

        [OperationContract]
        void SetProjectAdminFlag(Guid userId, Guid projectId, bool isProjectAdmin);

        [OperationContract]
        bool ChangeUserPassword(Guid userId, string oldPassword, string newPassword, string newPassword2);

        [OperationContract]
        [ApplyDataContractResolver]
        List<ProjectDO> ReadAdminProjectsForUser(Guid userId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<UserDO> ReadAllUsersByProject(Guid projectId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<ProjectUserDO> ReadAllProjectUsersInProject(Guid projectId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<ProjectDO> ReadProjectsForCompanyId(Guid companyId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<UserDO> ReadAllUsersForCompanyId(Guid companyId);

        [OperationContract]
        [ApplyDataContractResolver]
        CompanyDO ReadCompanyById(Guid id);

        [OperationContract]
        [ApplyDataContractResolver]
        void AddUserToProject(Guid userId, Guid projectId, bool isProjectAdmin);

        [OperationContract]
        [ApplyDataContractResolver]
        void RemoveUserFromProject(Guid userId, Guid projectId);

        [OperationContract]
        [ApplyDataContractResolver]
        ProjectDO CreateProject(ProjectDO project);

        [OperationContract]
        [ApplyDataContractResolver]
        ProjectDO UpdateProject(ProjectDO project);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteProject(ProjectDO project);

        [OperationContract]
        [ApplyDataContractResolver]
        UserDO CreateUser(UserDO user);

        [OperationContract]
        [ApplyDataContractResolver]
        UserDO UpdateUser(UserDO user);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteUser(UserDO user);
    }
}
