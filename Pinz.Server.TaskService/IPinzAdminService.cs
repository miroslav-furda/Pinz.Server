using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.Infrastructure;
using System.ServiceModel;

namespace Com.Pinz.Server.TaskService
{
    [ServiceContract]
    public interface IPinzAdminService
    {
        [OperationContract]
        [ApplyDataContractResolver]
        Company CreateCompany(Company company);

        [OperationContract]
        [ApplyDataContractResolver]
        void UpdateCompany(Company company);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteCompany(Company company);
    }
}
