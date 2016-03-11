using Com.Pinz.Server.DataAccess.Model;
using System;

namespace Com.Pinz.Server.DataAccess
{
    public interface ICompanyDAO : IBasicDAO<CompanyDO>
    {
        CompanyDO ReadCompanyByName(string companyName);
        CompanyDO ReadCompanyById(Guid id);
    }
}
