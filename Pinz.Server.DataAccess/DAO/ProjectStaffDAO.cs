using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Linq;
using System.Data.Entity;
using Com.Pinz.Server.DataAccess.Db;
using Ninject;
using System.Collections.Generic;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class ProjectStaffDAO : BasicDAO<ProjectStaffDO>, IProjectStaffDAO
    {
        [Inject]
        public ProjectStaffDAO(PinzDbContext context) : base(context) { }

        public void DeleteAllStaffingForUser(Guid userId)
        {
            var staffingToDelete = GetDbSet().Where(ps => ps.UserId == userId);
            foreach (var entity in staffingToDelete)
                Delete(entity);
        }

        public ProjectStaffDO GetById(Guid userId, Guid projectId)
        {
            return GetDbSet().Where(ps => ps.UserId == userId && ps.ProjectId == projectId).Single();
        }

        public bool IsUserAdminInProject(Guid userid, Guid projectId)
        {
            ProjectStaffDO staffing = GetDbSet().Where(ps => ps.UserId == userid && ps.ProjectId == projectId).SingleOrDefault();
            return staffing != null ? staffing.IsProjectAdmin : false;
        }

        public List<ProjectUserDO> ReadAllProjectUsersInProject(Guid projectId)
        {
            List<ProjectStaffDO> psList = GetDbSet().Where(ps => ps.ProjectId == projectId).ToList();
            List<ProjectUserDO> projectUserList = new List<ProjectUserDO>();
            foreach (ProjectStaffDO psDO in psList)
            {
                ProjectUserDO puDO = new ProjectUserDO(psDO.User);
                puDO.IsProjectAdmin = psDO.IsProjectAdmin;
                projectUserList.Add(puDO);
            }
            return projectUserList;
        }

        protected override DbSet<ProjectStaffDO> GetDbSet()
        {
            return context.ProjectStaff;
        }
    }
}
