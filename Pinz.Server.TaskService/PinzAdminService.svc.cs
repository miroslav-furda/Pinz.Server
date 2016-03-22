using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess;
using Ninject;
using System.Security.Permissions;

namespace Com.Pinz.Server.TaskService
{
    public class PinzAdminService : IPinzAdminService
    {
        private ICompanyDAO companyDAO;

        [Inject]
        public PinzAdminService (ICompanyDAO companyDAO)
        {
            this.companyDAO = companyDAO;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PINZ_SUPERADMIN")]
        public CompanyDO CreateCompany(CompanyDO company)
        {
            return companyDAO.Create(company);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PINZ_SUPERADMIN")]
        public void DeleteCompany(CompanyDO company)
        {
            companyDAO.Delete(company);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PINZ_SUPERADMIN")]
        [PrincipalPermission(SecurityAction.Demand, Role = "COMPANY_ADMIN")]
        public CompanyDO UpdateCompany(CompanyDO company)
        {
            return companyDAO.Update(company);
        }
    }
}
