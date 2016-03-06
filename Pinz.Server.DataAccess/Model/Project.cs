using Com.Pinzonline.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    public class Project : IProject
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
        public virtual Company Company { get; set; }

        public virtual List<Category> Categories { get; set; }
        public virtual List<ProjectStaff> ProjectStaff { get; set; }
    }
}
