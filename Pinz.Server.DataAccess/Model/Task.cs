
using com.Pinzonline.DomainModel;
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
        public TaskStatus Status { get; set; }

        [DataMember]
        public TaskPriority Priority { get; set; }

        [DataMember]
        public string Companies { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        ICategory ITask.Category
        {
            get
            {
                return Category;
            }

            set
            {
                Category = value as Category;
            }
        }

        public Guid UserId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IUser User
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
