using System;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.InviteUser;
using Common.Logging;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    public class SiteSubscriptionService : ISiteSubscriptionService
    {
        private readonly ILog _log = LogManager.GetLogger<SubscriptionNotificationService>();
        private readonly SubscriptionUtils _subscriptionUtils;
        private readonly IUserDAO _userDao;

        public SiteSubscriptionService(SubscriptionUtils subscriptionUtils, IUserDAO userDao)
        {
            _subscriptionUtils = subscriptionUtils;
            _userDao = userDao;
        }

        public void AddEvaluationSubscription(string name, string email, string companyname)
        {
            if (_userDao.ReadByEmail(email) == null)
            {
                var subscription = new Subscription
                {
                    SubscriptionReference = $"{email}_{DateTime.Now.ToLongDateString()}",
                    Status = SubscriptionStatus.Active,
                    StatusChanged = DateTime.Today,
                    Cancelable = true,
                    SourceName = "PINZonline.com",
                    SourceCampaign = "Trial",
                    Test = true,
                    Quantity = 1,
                    End = DateTime.Today.AddMonths(1),
                    Customer = new Customer
                    {
                        LastName = name,
                        Email = email,
                        Company = companyname
                    }
                };

                UserDO user = _subscriptionUtils.CreateSubscription(subscription, email);
                InvitationEmailSender.SendTrialInvitation(user.EMail, user.Password);
                _log.InfoFormat("Trial subsciption ID:{0}, email sent. Activation finished successfully.",
                    subscription.SubscriptionReference);
            }
        }
    }
}