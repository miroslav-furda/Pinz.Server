﻿using Com.Pinzonline.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    public class User : IUser
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        public string EMail { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public bool IsCompanyAdmin { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual List<Task> Tasks { get; set; }

        public virtual List<ProjectStaff> ProjectStaff { get; set; }
    }
}
