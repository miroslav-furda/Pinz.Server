using System;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.DataAccess
{
    public interface ICompanyDAO : IBasicDAO<CompanyDO>
    {
        CompanyDO ReadCompanyByName(string companyName);
        CompanyDO ReadCompanyById(Guid id);
        SubscriptionDO ReadSubscriptionByCompanyId(Guid companyId);
    }
}