using System.Xml.Serialization;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    public class Customer
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("company")]
        public string Company { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Company)}: {Company}, {nameof(Email)}: {Email}, {nameof(PhoneNumber)}: {PhoneNumber}";
        }
    }
}