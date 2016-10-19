using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using Common.Logging;

namespace Com.Pinz.Server.TaskService.FastSpring
{
    public class SubscriptionNotificationService : ISubscriptionNotificationService
    {
        private readonly ILog Log = LogManager.GetLogger<SubscriptionNotificationService>();


        public void Activate(string subscriptionReference)
        {
            var uri = GetFastSpringUri(subscriptionReference);
            var req = WebRequest.Create(uri);
            req.Method = "GET";
            req.Headers["Authorization"] = "Basic " +
                                           Convert.ToBase64String(
                                               Encoding.Default.GetBytes("contact@pinzonline.com:L2490CulusTO"));
            var resp = req.GetResponse();
            var s = new XmlSerializer(typeof(Subscription));

            var subscription = (Subscription)s.Deserialize(resp.GetResponseStream());
            Log.InfoFormat("Subscription {0} activated.", subscription);
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