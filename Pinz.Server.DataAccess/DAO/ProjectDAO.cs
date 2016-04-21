using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Ninject;
using Com.Pinz.Server.DataAccess.Db;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class ProjectDAO : BasicDAO<ProjectDO>, IProjectDAO
    {
        [Inject]
        public ProjectDAO(PinzDbContext context) : base(context) { }

        public List<ProjectDO> ReadProjectsForCompanyId(Guid companyId)
        {
            return GetDbSet().Where(p => p.CompanyId == companyId).ToList();
        }

        public List<ProjectDO> ReadAllProjectsForUserId(Guid userId)
        {
            return GetDbSet().Where(p => p.ProjectStaff.Any(ps => ps.UserId == userId)).ToList(); 
        }

        protected override DbSet<ProjectDO> GetDbSet()
        {
            return context.Projects;
        }

        public List<ProjectDO> ReadAdminProjectsForUserId(Guid userId)
        {
            return GetDbSet().Where(p => p.ProjectStaff.Any(ps => ps.UserId == userId && ps.IsProjectAdmin == true)).ToList();
        }

        public ProjectDO GetById(Guid projectId)
        {
            return GetDbSet().Where(p => p.ProjectId == projectId).Single();
        }
    }
}
