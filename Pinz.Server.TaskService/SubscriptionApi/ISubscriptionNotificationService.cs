using System.ServiceModel;
using System.ServiceModel.Web;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    [ServiceContract]
    public interface ISubscriptionNotificationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/callback/activate")]
        void Activate(FastSpringNotificationParameter parameter);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/callback/changed")]
        void Changed(FastSpringNotificationParameter parameter);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/callback/deactivated")]
        void Deactivated(FastSpringNotificationParameter parameter);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "/callback/paymentfailed")]
        void PaymentFailed(FastSpringNotificationParameter parameter);
    }
}