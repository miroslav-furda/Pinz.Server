using Com.Pinz.DomainModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    public class ProjectUserDO : IProjectUser
    {
        public ProjectUserDO(UserDO user)
        {
            UserId = user.UserId;
            CompanyId = user.CompanyId;
            EMail = user.EMail;
            FamilyName = user.FamilyName;
            FirstName = user.FirstName;
            IsCompanyAdmin = user.IsCompanyAdmin;
            IsPinzSuperAdmin = user.IsPinzSuperAdmin;
        }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public Guid CompanyId { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string EMail { get; set; }

        [Required]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string FamilyName { get; set; }

        [DataMember]
        public bool IsCompanyAdmin { get; set; }

        [DataMember]
        public bool IsPinzSuperAdmin { get; set; }

        [DataMember]
        public bool IsProjectAdmin { get; set; }
    }
}
