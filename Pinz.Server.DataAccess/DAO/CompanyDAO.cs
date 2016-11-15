using System;
using System.Data.Entity;
using System.Linq;
using Com.Pinz.Server.DataAccess.Db;
using Com.Pinz.Server.DataAccess.Model;
using Ninject;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class CompanyDAO : BasicDAO<CompanyDO>, ICompanyDAO
    {
        [Inject]
        public CompanyDAO(PinzDbContext context) : base(context)
        {
        }

        public CompanyDO ReadCompanyById(Guid id)
        {
            return GetDbSet().Single(c => c.CompanyId == id);
        }

        public SubscriptionDO ReadSubscriptionByCompanyId(Guid companyId)
        {
            return GetDbSet().Single(c => c.CompanyId == companyId).Subscription;
        }

        public CompanyDO ReadCompanyByName(string companyName)
        {
            return GetDbSet().Single(c => c.Name == companyName);
        }

        protected override DbSet<CompanyDO> GetDbSet()
        {
            return context.Companies;
        }
    }
}