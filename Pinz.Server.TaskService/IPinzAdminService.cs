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
        CompanyDO CreateCompany(CompanyDO company);

        [OperationContract]
        [ApplyDataContractResolver]
        CompanyDO UpdateCompany(CompanyDO company);

        [OperationContract]
        [ApplyDataContractResolver]
        void DeleteCompany(CompanyDO company);
    }
}
