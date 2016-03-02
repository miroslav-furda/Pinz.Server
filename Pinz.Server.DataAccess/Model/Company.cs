using Com.Pinzonline.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    public class Company : ICompany
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ComapnyId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }

        public virtual List<Project> Projects { get; set; }
    }
}
