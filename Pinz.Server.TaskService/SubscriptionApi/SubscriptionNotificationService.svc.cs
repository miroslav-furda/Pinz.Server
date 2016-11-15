using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using AutoMapper;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.InviteUser;
using Common.Logging;
using Ninject;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    public class SubscriptionNotificationService : ISubscriptionNotificationService
    {
        private readonly ICompanyDAO _companyDao;
        private readonly ILog _log = LogManager.GetLogger<SubscriptionNotificationService>();
        private readonly IMapper _mapper;
        private readonly ISubscriptionDAO _subscriptionDao;
        private readonly SubscriptionUtils _subscriptionUtils;
        private readonly IUserDAO _userDao;

        [Inject]
        public SubscriptionNotificationService(ISubscriptionDAO subscriptionDao, IMapper mapper,
            SubscriptionUtils subscriptionUtils, IUserDAO userDao, ICompanyDAO companyDao)
        {
            _subscriptionDao = subscriptionDao;
            _mapper = mapper;
            _subscriptionUtils = subscriptionUtils;
            _userDao = userDao;
            _companyDao = companyDao;
        }

        public void Activate(FastSpringNotificationParameter parameter)
        {
            var subscription = RetrieveSubscription(parameter.SubscriptionReference);
            _log.InfoFormat("Subscription {0} ACTIVATE request recieved.", subscription);

            if (subscription.ProductName.StartsWith("Basic"))
                subscription.Quantity = 1;
            else if (subscription.ProductName.StartsWith("Standard"))
                subscription.Quantity = 5;
            else if (subscription.ProductName.StartsWith("Large"))
                subscription.Quantity = 15;

            var user = _userDao.ReadByEmail(subscription.Customer.Email);
            if (user == null)
            {
                var newUser = _subscriptionUtils.CreateSubscription(subscription, subscription.Customer.Email);

                //send EMail
                InvitationEmailSender.SendNewCustomerInvitation(newUser.EMail, newUser.Password);
                _log.InfoFormat("Subscription ID:{0}, email sent. Activation finished successfully.",
                    subscription.SubscriptionReference);
            }
            else
            {
                var subscriptionDo = _companyDao.ReadSubscriptionByCompanyId(user.CompanyId);
                if (user.IsCompanyAdmin && subscriptionDo.Test)
                {
                    var newSubscriptionDo = _mapper.Map<SubscriptionDO>(subscription);
                    _log.InfoFormat("Subscription ID:{0}, subscription created in DB. {1}",
                        subscription.SubscriptionReference,
                        subscriptionDo);
                    var company = _companyDao.ReadCompanyById(user.CompanyId);
                    company.SubscriptionReference = newSubscriptionDo.SubscriptionReference;
                    company.Subscription = newSubscriptionDo;
                    _companyDao.Update(company);
                    _log.InfoFormat("Subscription ID:{0}, company updated in DB. {1}",
                        subscription.SubscriptionReference,
                        company);

                    _subscriptionDao.Delete(subscriptionDo);
                    InvitationEmailSender.SendTrialToSubscription(user.EMail, user.Password);
                }
                else
                {
                    var originalEmail = subscription.Customer.Email;
                    subscription.Customer.Email = FindNewCustomerLoginEmail(originalEmail);
                    var newUser = _subscriptionUtils.CreateSubscription(subscription, originalEmail);

                    //send EMail
                    InvitationEmailSender.SendNewCustomerDuplicateInvitation(originalEmail, newUser.EMail,
                        newUser.Password);
                    _log.InfoFormat("Subscription ID:{0}, email sent. Activation finished successfully.",
                        subscription.SubscriptionReference);
                }
            }
        }

        public void Changed(FastSpringNotificationParameter parameter)
        {
            UpdateSubscriptionInDb(parameter.SubscriptionReference, "CHANGE");
        }

        public void Deactivated(FastSpringNotificationParameter parameter)
        {
            var subscription = UpdateSubscriptionInDb(parameter.SubscriptionReference, "DEACTIVATED");
            InvitationEmailSender.SendGoodbye(subscription.Customer.Email);
        }

        public void PaymentFailed(FastSpringNotificationParameter parameter)
        {
            UpdateSubscriptionInDb(parameter.SubscriptionReference, "PAYMENT FAILED");
        }

        private string FindNewCustomerLoginEmail(string origEmail)
        {
            var regex = new Regex(@"(\.|[a-z]|[A-Z]|[0-9])*@(\.|[a-z]|[A-Z]|[0-9])*");
            var match = regex.Match(origEmail);

            var name = match.Groups[1].Value;
            var domain = match.Groups[2].Value;

            for (var count = 2; count < 100; count++)
            {
                var newEmail = $"{name}{count}@{domain}";
                if (_userDao.ReadByEmail(newEmail) == null)
                    return newEmail;
            }

            throw new InvalidOperationException($"Failed to create new email for existing user {origEmail}");
        }


        private Subscription UpdateSubscriptionInDb(string subscriptionReference, string operationName)
        {
            try
            {
                var subscription = RetrieveSubscription(subscriptionReference);
                _log.InfoFormat("Subscription {0} {1} request recieved.", subscription, operationName);

                var subscriptionDo = _subscriptionDao.ReadById(subscriptionReference);
                _mapper.Map(subscription, subscriptionDo);
                _subscriptionDao.Update(subscriptionDo);

                _log.InfoFormat("Subscription ID:{0}, subscription updated in DB. {1}", subscriptionReference,
                    subscriptionDo);

                return subscription;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Subscription RetrieveSubscription(string subscriptionReference)
        {
            var uri = GetFastSpringUri(subscriptionReference);
            var req = WebRequest.Create(uri);
            req.Method = "GET";
            req.Headers["Authorization"] = "Basic " +
                                           Convert.ToBase64String(
                                               Encoding.Default.GetBytes("contact@pinzonline.com:L2490CulusTO"));
            var resp = req.GetResponse();
            var s = new XmlSerializer(typeof(Subscription));

            var subscription = (Subscription) s.Deserialize(resp.GetResponseStream());
            return subscription;
        }

        private Uri GetFastSpringUri(string subscriptionReference)
        {
            var builder = new UriBuilder("https", "api.fastspring.com")
            {
                Path = $"company/pinzonline/subscription/{subscriptionReference}"
            };

            return builder.Uri;
        }
    }
}