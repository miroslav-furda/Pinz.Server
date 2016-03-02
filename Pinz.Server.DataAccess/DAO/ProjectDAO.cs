using Com.Pinz.Server.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Ninject;
using Com.Pinz.Server.DataAccess.Db;

namespace Com.Pinz.Server.DataAccess.DAO
{
    internal class ProjectDAO : BasicDAO<Project>, IProjectDAO
    {
        [Inject]
        public ProjectDAO(PinzDbContext context) : base(context) { }

        public List<Project> ReadProjectsForCompanyId(Guid companyId)
        {
            return GetDbSet().Where(p => p.CompanyId == companyId).ToList();
        }

        public List<Project> ReadAllProjectsForUserId(Guid userId)
        {
            return GetDbSet().Where(p => p.ProjectStaff.Any(ps => ps.UserId == userId)).ToList(); 
        }

        protected override DbSet<Project> GetDbSet()
        {
            return context.Projects;
        }
    }
}
