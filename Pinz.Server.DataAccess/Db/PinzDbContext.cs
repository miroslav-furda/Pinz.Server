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

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStaff> ProjectStaff { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectStaff>().HasRequired(ps => ps.User).WithMany( u => u.ProjectStaff).WillCascadeOnDelete(false);
        }
    }
}
