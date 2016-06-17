using Com.Pinz.DomainModel;
using System.Runtime.Serialization;

namespace Com.Pinz.Server.DataAccess.Model
{
    [DataContract]
    public class ProjectUserDO : UserDO, IProjectUser
    {
        [DataMember]
        public bool IsProjectAdmin { get; set; }

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
    }
}
