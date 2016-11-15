using System.Security.Permissions;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.Infrastructure;
using Ninject;

namespace Com.Pinz.Server.TaskService
{
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class PinzAdminService : IPinzAdminService
    {
        private readonly ICompanyDAO _companyDao;

        [Inject]
        public PinzAdminService(ICompanyDAO companyDao)
        {
            this._companyDao = companyDao;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PINZ_SUPERADMIN")]
        public CompanyDO CreateCompany(CompanyDO company)
        {
            return _companyDao.Create(company);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PINZ_SUPERADMIN")]
        public void DeleteCompany(CompanyDO company)
        {
            _companyDao.Delete(company);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PINZ_SUPERADMIN")]
        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public CompanyDO UpdateCompany(CompanyDO company)
        {
            var original = _companyDao.ReadCompanyById(company.CompanyId);
            company.SubscriptionReference = original.SubscriptionReference;
            return _companyDao.Update(company);
        }
    }
}