using System;
using System.Data.Entity;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using Com.Pinz.Server.DataAccess.Db;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService.Security
{
    public class UserSecurityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new SecurityTokenException("Username and password required");

            var dbContext = new PinzDbContext("pinzDBConnectionString");
            var user = dbContext.Users.SingleOrDefault(u => (u.EMail == userName) && (u.Password == password));
            if (user == null)
                throw new FaultException($"Wrong username ({userName}) or password");

            var company = dbContext.Companies.Single(c => c.CompanyId == user.CompanyId);
            var subscription =
                dbContext.Subscriptions.Single(s => s.SubscriptionReference == company.SubscriptionReference);
            if (subscription.Status == SubscriptionStatus.Inactive)
            {
                if (subscription.Test)
                {
                    throw new FaultException($"Trial for Company ({user.Company}) expired.");
                }
                else
                {
                    throw new FaultException($"Subscription for Company ({user.Company}) has been canceled.");
                }
            }
            if (subscription.Test && (subscription.End < DateTime.Today))
            {
                subscription.Status = SubscriptionStatus.Inactive;
                subscription.StatusReason = SubscriptionStatusReason.Canceled;
                dbContext.Entry(subscription).State = EntityState.Modified;
                dbContext.SaveChanges();

                throw new FaultException($"Trial for Company ({user.Company}) expired.");
            }
        }
    }
}