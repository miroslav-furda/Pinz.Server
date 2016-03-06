using Com.Pinzonline.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    public class ProjectStaff : IProjectStaff
    {
        [DataMember]
        [Key, Column(Order = 0)]
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [DataMember]
        [Key, Column(Order = 1)]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [DataMember]
        [Required]
        public bool IsProjectAdmin { get; set; }

    }
}
