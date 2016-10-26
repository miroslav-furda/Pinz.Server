using System.Data.Entity;
using System.Linq;
using Com.Pinz.Server.DataAccess.Db;
using Com.Pinz.Server.DataAccess.Model;
using Ninject;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class SubscriptionDAO : BasicDAO<SubscriptionDO>, ISubscriptionDAO
    {
        [Inject]
        public SubscriptionDAO(PinzDbContext context) : base(context)
        {
        }

        protected override DbSet<SubscriptionDO> GetDbSet()
        {
            return context.Subscriptions;
        }

        public SubscriptionDO ReadById(string subscriptionReference)
        {
            return GetDbSet().Single(s => s.Reference == subscriptionReference);
        }
    }
}