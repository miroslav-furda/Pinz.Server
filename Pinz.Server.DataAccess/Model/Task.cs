﻿using Com.Pinzonline.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    public class Task : ITask
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TaskId { get; set; }

        [DataMember]
        [Required]
        public string TaskName { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public bool IsComplete { get; set; }

        [DataMember]
        [Required]
        public DateTime CreationTime { get; set; }

        [DataMember]
        public DateTime? DateCompleted { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? DueDate { get; set; }

        [DataMember]
        public int ActualWork { get; set; }

        [DataMember]
        [Required]
        public TaskStatus Status { get; set; }

        [DataMember]
        public TaskPriority? Priority { get; set; }

        [Required]
        [DataMember]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [DataMember]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
