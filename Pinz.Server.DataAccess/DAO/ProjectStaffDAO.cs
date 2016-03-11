using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Linq;
using System.Data.Entity;
using Com.Pinz.Server.DataAccess.Db;
using Ninject;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class ProjectStaffDAO : BasicDAO<ProjectStaffDO>, IProjectStaffDAO
    {
        [Inject]
        public ProjectStaffDAO(PinzDbContext context) : base(context) { }

        public ProjectStaffDO GetById(Guid userId, Guid projectId)
        {
            return GetDbSet().Where(ps => ps.UserId == userId && ps.ProjectId == projectId).Single();
        }

        public bool IsUserAdminInProject(Guid userid, Guid projectId)
        {
            ProjectStaffDO staffing = GetDbSet().Where(ps => ps.UserId == userid && ps.ProjectId == projectId).SingleOrDefault();
            return staffing != null ? staffing.IsProjectAdmin : false;
        }

        protected override DbSet<ProjectStaffDO> GetDbSet()
        {
            return context.ProjectStaff;
        }
    }
}
