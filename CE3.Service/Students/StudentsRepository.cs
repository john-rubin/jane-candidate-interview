using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CE3.Service.Data;
using CE3.Service.Exceptions;

namespace CE3.Service.Students
{
	public interface IStudentsRepository
	{
		Task<Student> CreateStudent(Student student);
		Task<Student> GetStudent(int id);
		Task<Student> GetStudent(String firstName, String lastName);
		Task<Student> ChangeLastName(int id, String newLastName);

	}

	public class StudentsRepository : IStudentsRepository
	{
		private readonly IUniversityDbFactory _universityDbFactory;

		public StudentsRepository(IUniversityDbFactory universityDbFactory)
		{
			_universityDbFactory = universityDbFactory;
		}

		public async Task<Student> ChangeLastName(int id, string newLastName)
		{
			var dbContext = _universityDbFactory.GetDbContext();
			var student = dbContext.Students.FirstOrDefault(s => s.Id == id);
			student.LastName = newLastName;

			var existingStudent = await GetStudent(student.FirstName, student.LastName);
			if (existingStudent != null)
			{
				throw new InvalidOperationException();
			}
			var result = await dbContext.SaveChangesAsync();
			return student;
			//throw new NotImplementedException();
		}

		public async Task<Student> CreateStudent(Student student)
		{
			var dbContext = _universityDbFactory.GetDbContext();
			var existingStudent = await GetStudent(student.FirstName, student.LastName);
			if (existingStudent != null)
			{
				throw new InvalidOperationException();
			}
			var newStudent = dbContext.Students.Add(student);
			await dbContext.SaveChangesAsync();
			return newStudent;

			//throw new NotImplementedException();
		}

		public async Task<Student> GetStudent(int id)
		{
			var dbContext = _universityDbFactory.GetDbContext();
			var student = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
			if (student == null)
			{
				throw new NotFoundException();
			}
			return student;

			//throw new NotImplementedException();
		}

		public async Task<Student> GetStudent(string firstName, string lastName)
		{
			var dbContext = _universityDbFactory.GetDbContext();
			var student = dbContext.Students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
			if (student == null)
			{
				throw new NotFoundException();
			}
			return student;
			//throw new NotImplementedException();
		}
	}
}