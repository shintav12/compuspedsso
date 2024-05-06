using System.Data.Entity;

namespace CompuSPED.DataTranser.Databases.CSPED
{
    public partial class CSPEDContext : DbContext
    {
        public CSPEDContext()
            : base("name=CSPED")
        {
        }

        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
