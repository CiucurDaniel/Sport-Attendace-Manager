using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using SportAttendanceSystem.Models;

namespace SportAttendanceSystem.DataAccessLayer
{
    public class SportAttendanceManagerContext : DbContext
    {
        public SportAttendanceManagerContext() : base("SportAttendanceManagerDatabase")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<SportAttendanceManagerContext>());
            // In case of migration errors or changes to model uncomment the above initializer

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SportAttendanceManagerContext>());
            //Database.SetInitializer(new SportAttendanceManagerInitializer()); // BUG: Does no work throws: entity error when adding Users in the seed method
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }
    }
}