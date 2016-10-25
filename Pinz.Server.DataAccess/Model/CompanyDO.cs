using Com.Pinz.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    [Table("Companies")]
    public class CompanyDO : ICompany
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompanyId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        public virtual List<UserDO> Users { get; set; }

        public virtual List<ProjectDO> Projects { get; set; }

        [Required]
        public Guid SubscriptionId { get; set; }
        [ForeignKey("SubscriptionReference")]
        public virtual SubscriptionDO Subscription { get; set; }
    }
}
