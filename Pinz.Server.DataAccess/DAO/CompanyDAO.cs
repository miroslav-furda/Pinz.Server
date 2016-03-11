using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Linq;
using System.Data.Entity;
using Ninject;
using Com.Pinz.Server.DataAccess.Db;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class CompanyDAO : BasicDAO<CompanyDO>, ICompanyDAO
    {
        [Inject]
        public CompanyDAO(PinzDbContext context) : base(context) { }

        public CompanyDO ReadCompanyById(Guid id)
        {
            return GetDbSet().Where(c => c.CompanyId == id).Single();
        }

        public CompanyDO ReadCompanyByName(string companyName)
        {
            return GetDbSet().Where(c => c.Name == companyName).Single();
        }

        protected override DbSet<CompanyDO> GetDbSet()
        {
            return context.Companies;
        }
    }
}
