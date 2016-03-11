using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.DataAccess;
using Ninject;

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

        public CompanyDO CreateCompany(CompanyDO company)
        {
            return companyDAO.Create(company);
        }

        public void DeleteCompany(CompanyDO company)
        {
            companyDAO.Delete(company);
        }

        public void UpdateCompany(CompanyDO company)
        {
            companyDAO.Update(company);
        }
    }
}
