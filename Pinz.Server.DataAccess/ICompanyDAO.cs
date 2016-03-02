using Com.Pinz.Server.DataAccess.Model;
using System;

namespace Com.Pinz.Server.DataAccess
{
    public interface ICompanyDAO : IBasicDAO<Company>
    {
        Company ReadCompanyByName(string companyName);
        Company ReadCompanyById(Guid id);
    }
}
