using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CompuSPED.SBData
{
    public partial class KansasGBContext : DbContext
    {
        public KansasGBContext(): base("KansasGB")
        {
        }

        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserDistrict> UserDistricts { get; set; }
        public virtual DbSet<UserSchool> UserSchools { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .Property(e => e.DistrictName)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.DistrictNameLong)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.DistrictCode)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.StreetAddress)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.CoopCode)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.EligibilityFlag)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.SchoolName)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.SchoolCode)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.StreetAddress)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Principal)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.StudentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.UIC)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MiddleInitial)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.StreetAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.SState)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.ParentName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MedicaidBeneficiaryID)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MedicaidLastName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MedicaidFirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MedicaidMI)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.SSN)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.MedicaidGender)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Bus1)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Bus2)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.AccessLevel)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CoopCode)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.QlikUserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.UserRoleDesc)
                .IsUnicode(false);
        }
    }
}
