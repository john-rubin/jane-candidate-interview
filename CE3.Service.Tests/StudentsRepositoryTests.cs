using System;
using System.Linq;
using System.Threading.Tasks;
using CE3.Service.Data;
using CE3.Service.Students;
using CE3.Service.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CE3.Service.Tests
{
	[TestClass]
	public class StudentsRepositoryTests
	{
		private IUniversityDbContext _universityDbContext;
		private IStudentsRepository _studentRepository;

		[TestInitialize]
		public void Init()
		{
			var universityDbFactory = new UniversitySqlLiteDbFactory();
			_universityDbContext = universityDbFactory.GetDbContext();
			_studentRepository = new StudentsRepository(universityDbFactory);
		}

		[TestMethod]
		public async Task CreateStudent_Success()
		{
			var newStudent = new Student{ FirstName = "FName", LastName = "LName"};
			var result = await _studentRepository.CreateStudent(newStudent);

			var addedStudent = _universityDbContext.Students.FirstOrDefault(s => s.Id.Equals(result.Id));

			Assert.IsNotNull(addedStudent);
			Assert.AreEqual(newStudent.FirstName, addedStudent.FirstName);
			Assert.AreEqual(newStudent.LastName, addedStudent.LastName);
		}

		[TestMethod]
		public async Task GetStudent_Success()
		{
			var student = _universityDbContext.Students.Add(new Student() {FirstName = "Test", LastName = "Get"});
			_universityDbContext.SaveChanges();
			var retrievedStudent = await _studentRepository.GetStudent(student.Id);

			Assert.IsNotNull(retrievedStudent);
			Assert.AreEqual(student.Id, retrievedStudent.Id);
			Assert.AreEqual("Test", student.FirstName);
			Assert.AreEqual("Get", student.LastName);
		}

		[TestMethod]
		public async Task UpdateStudent_Success()
		{
			var student = _universityDbContext.Students.Add(new Student() { FirstName = "Name", LastName = "Original" });
			_universityDbContext.SaveChanges();

			var x = await _studentRepository.ChangeLastName(student.Id, "New");

			var updatedStudent = _universityDbContext.Students.AsNoTracking().FirstOrDefault(s => s.Id.Equals(student.Id));
			Assert.IsNotNull(updatedStudent);
			Assert.AreEqual("New", updatedStudent.LastName);
		}

	}
}
