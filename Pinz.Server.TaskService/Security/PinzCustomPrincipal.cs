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
        private static string[] _roles = new string[] { "USER", "PROJECT_ADMIN", "COMPANY_ADMIN", "SUPERADMIN" };


        public PinzCustomPrincipal(IIdentity client)
        {
            this.Identity = client;
        }

        public bool IsInRole(string role)
        {
            PinzDbContext dbContext = new PinzDbContext();
            List<string> roles = new List<string>();
            if (Identity.IsAuthenticated)
                roles.Add("USER");
            UserDO user = dbContext.Users.Where(u => u.EMail == Identity.Name).Single();
            if (user.IsCompanyAdmin)
                roles.Add("COMPANY_ADMIN");

            return roles.Contains(role);
        }
    }
}