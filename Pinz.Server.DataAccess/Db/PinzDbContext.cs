using Com.Pinz.Server.DataAccess.Model;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.Db
{
    public class PinzDbContext : DbContext
    {
        public PinzDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PinzDbContext, Migrations.Configuration>(connectionString));

        }

        public PinzDbContext() : base("name=pinzDBConnectionString")
        {
        }

        public DbSet<TaskDO> Tasks { get; set; }
        public DbSet<CategoryDO> Categories { get; set; }
        public DbSet<ProjectDO> Projects { get; set; }
        public DbSet<ProjectStaffDO> ProjectStaff { get; set; }
        public DbSet<CompanyDO> Companies { get; set; }
        public DbSet<UserDO> Users { get; set; }
        public DbSet<SubscriptionDO> Subscriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectStaffDO>().HasRequired(ps => ps.User).WithMany( u => u.ProjectStaff).WillCascadeOnDelete(false);
        }
    }
}
