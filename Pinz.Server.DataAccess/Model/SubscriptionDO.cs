using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [Table("Subscriptions")]
    public class SubscriptionDO
    {

        [Key]
        public string Reference { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DateTime StatusChanged { get; set; }

        public SubscriptionStatusReason? StatusReason { get; set; }

        public Boolean Cancelable { get; set; }

        public Boolean Test { get; set; }

        public string Referrer { get; set; }

        public string SourceName { get; set; }

        public string SourceKey { get; set; }

        public string SourceCampaign { get; set; }

        public string CustomerUrl { get; set; }

        public string ProductName { get; set; }

        public string Tags { get; set; }

        public int Quantity { get; set; }

        public string Coupon { get; set; }

        public DateTime NextPeriodDate { get; set; }

        public DateTime End { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual CompanyDO Company { get; set; }

        public override string ToString()
        {
            return $"{nameof(Status)}: {Status}, {nameof(StatusChanged)}: {StatusChanged}, {nameof(StatusReason)}: {StatusReason}, " +
                   $"{nameof(Cancelable)}: {Cancelable}, {nameof(Test)}: {Test}, {nameof(Reference)}: {Reference}, {nameof(Referrer)}: {Referrer}, " +
                   $"{nameof(SourceName)}: {SourceName}, {nameof(SourceKey)}: {SourceKey}, {nameof(SourceCampaign)}: {SourceCampaign}, " +
                   $"{nameof(CustomerUrl)}: {CustomerUrl}, {nameof(ProductName)}: {ProductName}, {nameof(Tags)}: {Tags}, " +
                   $"{nameof(Quantity)}: {Quantity}, {nameof(Coupon)}: {Coupon}, {nameof(NextPeriodDate)}: {NextPeriodDate}, {nameof(End)}: {End}";
        }
    }


    public enum SubscriptionStatus
    {
        [XmlEnum(Name = "active")]
        Active,
        [XmlEnum(Name = "inactive")]
        Inactive
    }

    public enum SubscriptionStatusReason
    {
        [XmlEnum(Name = "canceled-non-payment")]
        CanceledNonPayment,
        [XmlEnum(Name = "completed")]
        Completed,
        [XmlEnum(Name = "canceled")]
        Canceled
    }
}