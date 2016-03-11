using Com.Pinzonline.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    [Table("Projects")]
    public class ProjectDO : IProject
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProjectId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual CompanyDO Company { get; set; }

        public virtual List<CategoryDO> Categories { get; set; }
        public virtual List<ProjectStaffDO> ProjectStaff { get; set; }
    }
}
