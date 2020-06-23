using System;
using System.Linq;
using System.Threading.Tasks;
using CE3.Service.Data;
using CE3.Service.Models;
using CE3.Service.Repositories;
using CE3.Service.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CE3.Service.Tests
{
	[TestClass]
	public class CourseOfferingsServiceTests
	{
		private IUniversityDbContext _universityDbContext;
		private ICourseOfferingsServerice _courseOfferingsServerice;
		private UniversitySqlLiteDbFactory _universitySqlLiteDbFactory;

		[TestInitialize]
		public void Init()
		{
			_universitySqlLiteDbFactory = new UniversitySqlLiteDbFactory();
			_universityDbContext = _universitySqlLiteDbFactory.GetDbContext();
			_courseOfferingsServerice = new CourseOfferingsServerice(
				new CourseOfferingsRepository(_universitySqlLiteDbFactory));
		}

		[TestMethod]
		public async Task CreateCourse_Success()
		{
			var department = _universityDbContext.Departments.Add(new Department { Name = "D1" });
			var course = _universityDbContext.Courses.Add(new Course {DepartmentId = department.Id, Name = "Course 1"});
			var semester = _universityDbContext.Semesters.Add(new Semester {Year = 2020, Season = "Fall"});
			_universityDbContext.SaveChanges();

			var courseOffering = await _courseOfferingsServerice.CreateCourseOffering(new CourseOffering() { CourseId = course.Id, SemesterId = semester.Id });

			var addedCourse = _universityDbContext.Courses.FirstOrDefault(c => c.Id.Equals(courseOffering.Id));

			Assert.IsNotNull(addedCourse);
		}
	}
}
