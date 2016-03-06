using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.Infrastructure;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Com.Pinz.Server.TaskService
{
    [ServiceContract(Namespace ="http://pinzonline.com/services")]
    public interface ITaskService
    {
        [OperationContract]
        [ApplyDataContractResolver]
        List<Task> ReadAllTasksByCategoryId(Guid categoryId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<Category> ReadAllCategoriesByProjectId(Guid projectId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<Project> ReadAllProjectsForUserId(Guid userId);

        [OperationContract]
        [ApplyDataContractResolver]
        Task CreateTask(Task task);

        [OperationContract]
        [ApplyDataContractResolver]
        void UpdateTask(Task task);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteTask(Task task);

        [OperationContract]
        [ApplyDataContractResolver]
        Category CreateCategory(Category category);

        [OperationContract]
        [ApplyDataContractResolver]
        void UpdateCategory(Category category);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteCategory(Category category);
    }
}
