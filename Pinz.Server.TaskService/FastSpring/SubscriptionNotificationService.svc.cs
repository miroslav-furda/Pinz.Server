using System;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.InviteUser;
using Common.Logging;
using Ninject;

namespace Com.Pinz.Server.TaskService.FastSpring
{
    public class SubscriptionNotificationService : ISubscriptionNotificationService
    {
        private readonly ILog Log = LogManager.GetLogger<SubscriptionNotificationService>();

        private readonly ICompanyDAO _companyDao;
        private readonly IMapper _mapper;
        private readonly ISubscriptionDAO _subscriptionDAO;
        private readonly IUserDAO _userDAO;

        [Inject]
        public SubscriptionNotificationService(ICompanyDAO companyDao, IUserDAO userDao,
            ISubscriptionDAO subscriptionDao, IMapper mapper)
        {
            _companyDao = companyDao;
            _userDAO = userDao;
            _subscriptionDAO = subscriptionDao;
            _mapper = mapper;
        }

        public void Activate(string subscriptionReference)
        {
            var subscription = RetrieveSubscription(subscriptionReference);
            Log.InfoFormat("Subscription {0} ACTIVATE request recieved.", subscription);

            //create Company
            var company = new CompanyDO {Name = subscription.Customer.Company};
            _companyDao.Create(company);
            Log.InfoFormat("Subscription ID:{0}, company created in DB. {1}", subscriptionReference, company);

            //create Subscription
            var subscriptionDo = _mapper.Map<SubscriptionDO>(subscription);
            subscriptionDo.Company = company;
            _subscriptionDAO.Create(subscriptionDo);
            Log.InfoFormat("Subscription ID:{0}, subscription created in DB. {1}", subscriptionReference, subscriptionDo);

            //create master User
            var user = new UserDO();
            user.EMail = subscription.Customer.Email;
            user.FirstName = subscription.Customer.FirstName;
            user.FamilyName = subscription.Customer.LastName;
            user.PhoneNumber = subscription.Customer.PhoneNumber;
            user.IsCompanyAdmin = true;
            user.Company = company;
            user.IsPinzSuperAdmin = false;
            user.Password = RandomPassword.Generate();
            user = _userDAO.Create(user);
            Log.InfoFormat("Subscription ID:{0}, user created in DB. {1}", subscriptionReference, user);

            //send EMail
            InvitationEmailSender.SendNewCustomerInvitation(user.EMail, user.Password);
            Log.InfoFormat("Subscription ID:{0}, email sent. Activation finished successfully.", subscriptionReference);
        }

        public void Changed(string subscriptionReference)
        {
            UpdateSubscriptionInDb(subscriptionReference, "CHANGE");
        }

        public void Deactivated(string subscriptionReference)
        {
            var subscription = UpdateSubscriptionInDb(subscriptionReference, "DEACTIVATED");
            InvitationEmailSender.SendGoodbye(subscription.Customer.Email);
        }

        public void PaymentFailed(string subscriptionReference)
        {
            UpdateSubscriptionInDb(subscriptionReference, "PAYMENT FAILED");
        }

        private Subscription UpdateSubscriptionInDb(string subscriptionReference, string operationName)
        {
            var subscription = RetrieveSubscription(subscriptionReference);
            Log.InfoFormat("Subscription {0} {1} request recieved.", subscription, operationName);

            var subscriptionDo = _subscriptionDAO.ReadById(subscriptionReference);
            _mapper.Map(subscription, subscriptionDo);
            _subscriptionDAO.Update(subscriptionDo);

            Log.InfoFormat("Subscription ID:{0}, subscription updated in DB. {1}", subscriptionReference, subscriptionDo);

            return subscription;
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