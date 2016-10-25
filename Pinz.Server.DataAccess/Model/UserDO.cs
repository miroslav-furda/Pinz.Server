using Com.Pinz.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    [Table("Users")]
    public class UserDO : IUser
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [DataMember]
        [Required]
        [EmailAddress]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string EMail { get; set; }

        [Required]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string FamilyName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public bool IsCompanyAdmin { get; set; }

        [DataMember]
        public bool IsPinzSuperAdmin { get; set; }

        [DataMember]
        [Required]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual CompanyDO Company { get; set; }

        public virtual List<TaskDO> Tasks { get; set; }

        public virtual List<ProjectStaffDO> ProjectStaff { get; set; }
    }
}
