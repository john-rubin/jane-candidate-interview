using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CE3.Service.Students;

namespace CE3.Service.Data
{
	public interface IUniversityDbContext : IDisposable
	{
		//DbSet<Course> Courses { get; set; }
		//DbSet<CourseOffering> CourseOfferings { get; set; }
		//DbSet<Department> Departments { get; set; }
		////DbSet<Enrollment> Enrollments { get; set; }
		//DbSet<Semester> Semesters { get; set; }
		DbSet<Student> Students { get; set; }

		int SaveChanges();
		Task<int> SaveChangesAsync();

	}
}