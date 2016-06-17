using Com.Pinz.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    [Table("ProjectStaffs")]
    public class ProjectStaffDO : IProjectStaff
    {
        [DataMember]
        [Key, Column(Order = 0)]
        public Guid ProjectId { get; set; }
        public virtual ProjectDO Project { get; set; }

        [DataMember]
        [Key, Column(Order = 1)]
        public Guid UserId { get; set; }
        public virtual UserDO User { get; set; }

        [DataMember]
        [Required]
        public bool IsProjectAdmin { get; set; }

    }
}
