using System.ServiceModel;
using System.ServiceModel.Web;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    [ServiceContract]
    public interface ISiteSubscriptionService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "/addevalsubscription")]
        void AddEvaluationSubscription(string name, string email, string companyname);
    }
}