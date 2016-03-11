using Com.Pinzonline.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    [Table("Categories")]
    public class CategoryDO : ICategory
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectDO Project { get; set; }


        public virtual List<TaskDO> Tasks { get; set; }

    }
}
