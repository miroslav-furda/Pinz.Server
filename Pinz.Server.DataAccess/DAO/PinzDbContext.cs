using Com.Pinz.Server.DataAccess.Model;
using System.Data.Entity;

namespace Com.Pinz.Server.DataAccess.DAO
{
    public class PinzDbContext : DbContext
    {
        public PinzDbContext(string connectionString) : base(connectionString)
        {
        }

        public PinzDbContext() : base("name=pinzDBConnectionString")
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
