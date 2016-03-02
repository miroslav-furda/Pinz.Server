using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Linq;
using System.Data.Entity;
using Com.Pinz.Server.DataAccess.Db;
using Ninject;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class ProjectStaffDAO : BasicDAO<ProjectStaff>, IProjectStaffDAO
    {
        [Inject]
        public ProjectStaffDAO(PinzDbContext context) : base(context) { }

        public bool IsUserAdminInProject(Guid userid, Guid projectId)
        {
            return GetDbSet().Where( ps => ps.UserId == userid && ps.ProjectId== projectId).Single().IsProjectAdmin;
        }

        protected override DbSet<ProjectStaff> GetDbSet()
        {
            return context.ProjectStaff;
        }
    }
}
