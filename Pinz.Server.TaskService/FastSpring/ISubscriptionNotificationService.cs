using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Com.Pinz.Server.TaskService.FastSpring
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISubscriptionNotificationService" in both code and config file together.
    [ServiceContract]
    public interface ISubscriptionNotificationService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "callback/activate?subscriptionReference={subscriptionReference}")]
        void Activate(string subscriptionReference );
    }
}
