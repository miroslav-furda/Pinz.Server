using System.ServiceModel;
using System.ServiceModel.Web;

namespace Com.Pinz.Server.TaskService.FastSpring
{
    [ServiceContract]
    public interface ISubscriptionNotificationService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "callback/activate?subscriptionReference={subscriptionReference}")]
        void Activate(string subscriptionReference);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "callback/changed?subscriptionReference={subscriptionReference}")]
        void Changed(string subscriptionReference);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "callback/deactivated?subscriptionReference={subscriptionReference}")]
        void Deactivated(string subscriptionReference);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "callback/paymentfailed?subscriptionReference={subscriptionReference}")]
        void PaymentFailed(string subscriptionReference);
    }
}