using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
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

        private ICompanyDAO _companyDao;
        private IUserDAO _userDAO;
        private ISubscriptionDAO _subscriptionDAO;

        [Inject]
        public SubscriptionNotificationService(ICompanyDAO companyDao, IUserDAO userDao, ISubscriptionDAO subscriptionDao)
        {
            _companyDao = companyDao;
            _userDAO = userDao;
            _subscriptionDAO = subscriptionDao;
        }

        public void Activate(string subscriptionReference)
        {
            var subscription = RetrieveSubscription(subscriptionReference);
            Log.InfoFormat("Subscription {0} actiavtion request recieved.", subscription);

            //create Company
            CompanyDO company = new CompanyDO {Name = subscription.Customer.Company};
            _companyDao.Create(company);

            //create Subscription
            SubscriptionDO subscriptionDo = new SubscriptionDO()
            {
                Reference = subscription.Reference,
                Company = company
            };
            _subscriptionDAO.Create(subscriptionDo);

            //create master User
            UserDO user = new UserDO();
            user.EMail = subscription.Customer.Email;
            user.FirstName = subscription.Customer.FirstName;
            user.FamilyName = subscription.Customer.LastName;
            user.PhoneNumber = subscription.Customer.PhoneNumber;
            user.IsCompanyAdmin = true;
            user.Company = company;
            user.IsPinzSuperAdmin = false;
            user.Password =  RandomPassword.Generate();

            user = _userDAO.Create(user);

            //send EMail
            InvitationEmailSender.SendNewCustomerInvitation(user.EMail, user.Password);

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