using System.Runtime.Serialization;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    [DataContract(Namespace = "http://xml.fastspring.com/template/implied", Name = "parameters")]
    public class FastSpringNotificationParameter
    {
        [DataMember(Name = "subscriptionReference")]
        public string SubscriptionReference { get; set; }
    }
}