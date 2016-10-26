using System;
using Com.Pinz.Server.DataAccess.Db;
using System.Linq;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService.Security
{
    public class UserSecurityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new SecurityTokenException("Username and password required");


            PinzDbContext dbContext = new PinzDbContext();
            UserDO user = dbContext.Users.SingleOrDefault(u => u.EMail == userName && u.Password == password);
            if (user == null)
            {
                throw new FaultException($"Wrong username ({userName}) or password");
            }
            else if (user.Company.Subscription.Status == SubscriptionStatus.Inactive )
            {
                SubscriptionDO subscription = user.Company.Subscription;
                if (subscription.StatusReason != null && subscription.StatusReason == SubscriptionStatusReason.Canceled &&
                    subscription.End.AddMonths(1) < DateTime.Today)
                {
                    throw new FaultException($"Subscription for Company ({user.Company}) has been canceled.");
                }else if (subscription.StatusReason != null && subscription.StatusReason == SubscriptionStatusReason.Canceled &&
                    subscription.End.AddMonths(2) < DateTime.Today)
                {
                    throw new FaultException($"Payment for Company ({user.Company}) could NOT be processed.");
                }

            }
        }
    }
}