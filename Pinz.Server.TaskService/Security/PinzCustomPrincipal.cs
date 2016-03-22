using Com.Pinz.Server.DataAccess.Db;
using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Com.Pinz.Server.TaskService.Security
{
    public class PinzCustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; }
        private List<string> roles;


        public PinzCustomPrincipal(IIdentity client)
        {
            this.Identity = client;

            PinzDbContext dbContext = new PinzDbContext();
            roles = new List<string>();
            if (Identity.IsAuthenticated)
            {
                roles.Add(UserSecurityRole.USER.ToString());
            }

            UserDO user = dbContext.Users.Where(u => u.EMail == Identity.Name).Single();
            if (user.ProjectStaff.Any(ps => ps.IsProjectAdmin == true))
            {
                roles.Add(UserSecurityRole.PROJECT_ADMIN.ToString());
            }
            if (user.IsCompanyAdmin)
            {
                roles.Add(UserSecurityRole.PROJECT_ADMIN.ToString());
                roles.Add(UserSecurityRole.COMPANY_ADMIN.ToString());
            }
            if (user.IsPinzSuperAdmin)
            {
                roles.Add(UserSecurityRole.PROJECT_ADMIN.ToString());
                roles.Add(UserSecurityRole.COMPANY_ADMIN.ToString());
                roles.Add(UserSecurityRole.PINZ_SUPERADMIN.ToString());
            }
        }

        public bool IsInRole(string role)
        {
            return roles.Contains(role);
        }
    }
}