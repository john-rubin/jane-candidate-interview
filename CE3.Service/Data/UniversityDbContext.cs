using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CE3.Service.Students;

namespace CE3.Service.Data
{
    public class UniversityDbContext : DbContext, IUniversityDbContext
    {
        public UniversityDbContext() : base("UniversityContext") { }
        public UniversityDbContext(string connectionString) : base(connectionString) { }
        public UniversityDbContext(DbConnection connection) : base(connection, true) { }

        //public DbSet<Course> Courses { get; set; }
        //public DbSet<CourseOffering> CourseOfferings { get; set; }
        //public DbSet<Department> Departments { get; set; }
        ////public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}