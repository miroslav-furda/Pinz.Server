using System;
using System.Xml.Serialization;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    [XmlRoot("subscription")]
    public class Subscription
    {
        [XmlElement("status")]
        public SubscriptionStatus Status { get; set; }

        [XmlElement("statusChanged")]
        public DateTime StatusChanged { get; set; }

        [XmlElement("statusReason")]
        public SubscriptionStatusReason? StatusReason { get; set; }

        [XmlElement("cancelable")]
        public Boolean Cancelable { get; set; }

        [XmlElement("test")]
        public Boolean Test { get; set; }

        [XmlElement("reference")]
        public string SubscriptionReference { get; set; }

        [XmlElement("referrer")]
        public string Referrer { get; set; }

        [XmlElement("sourceName")]
        public string SourceName { get; set; }

        [XmlElement("sourceKey")]
        public string SourceKey { get; set; }

        [XmlElement("sourceCampaign")]
        public string SourceCampaign { get; set; }

        [XmlElement("customer")]
        public Customer Customer { get; set; }

        [XmlElement("customerUrl")]
        public string CustomerUrl { get; set; }

        [XmlElement("productName")]
        public string ProductName { get; set; }

        [XmlElement("tags")]
        public string Tags { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [XmlElement("coupon")]
        public string Coupon { get; set; }

        [XmlElement("nextPeriodDate")]
        public DateTime? NextPeriodDate { get; set; }

        [XmlElement("end")]
        public DateTime? End { get; set; }

        public override string ToString()
        {
            return $"{nameof(Status)}: {Status}, {nameof(StatusChanged)}: {StatusChanged}, {nameof(StatusReason)}: {StatusReason}, " +
                   $"{nameof(Cancelable)}: {Cancelable}, {nameof(Test)}: {Test}, {nameof(SubscriptionReference)}: {SubscriptionReference}, {nameof(Referrer)}: {Referrer}, " +
                   $"{nameof(SourceName)}: {SourceName}, {nameof(SourceKey)}: {SourceKey}, {nameof(SourceCampaign)}: {SourceCampaign}, {nameof(Customer)}: " +
                   $"{Customer}, {nameof(CustomerUrl)}: {CustomerUrl}, {nameof(ProductName)}: {ProductName}, {nameof(Tags)}: {Tags}, {nameof(Quantity)}: " +
                   $"{Quantity}, {nameof(Coupon)}: {Coupon}, {nameof(NextPeriodDate)}: {NextPeriodDate}, {nameof(End)}: {End}";
        }
    }

}