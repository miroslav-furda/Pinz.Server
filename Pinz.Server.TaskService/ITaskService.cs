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
        List<TaskDO> ReadAllTasksByCategoryId(Guid categoryId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<CategoryDO> ReadAllCategoriesByProjectId(Guid projectId);

        [OperationContract]
        [ApplyDataContractResolver]
        List<ProjectDO> ReadAllProjectsForUserEmail(string email);

        [OperationContract]
        [ApplyDataContractResolver]
        TaskDO CreateTask(TaskDO task, string userEmail);

        [OperationContract]
        [ApplyDataContractResolver]
        TaskDO UpdateTask(TaskDO task);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteTask(TaskDO task);

        [OperationContract]
        [ApplyDataContractResolver]
        CategoryDO CreateCategory(CategoryDO category);

        [OperationContract]
        [ApplyDataContractResolver]
        CategoryDO UpdateCategory(CategoryDO category);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteCategory(CategoryDO category);
    }
}
