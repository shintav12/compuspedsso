using CompuSPED.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CompuSPED.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext() : base("CompuSPEDDatabaseContext")
        {
        }

        public DbSet<Districts> Districts { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<LoginHistory> Logins { get; set; }
        public DbSet<ErrorLog> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}
