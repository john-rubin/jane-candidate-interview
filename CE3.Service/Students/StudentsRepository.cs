using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CE3.Service.Data;

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

		public Task<Student> ChangeLastName(int id, string newLastName)
		{
			throw new NotImplementedException();
		}

		public async Task<Student> CreateStudent(Student student)
		{
			using (var context = _universityDbFactory.GetDbContext())
			{
				var dbStudent = await GetStudent(student.FirstName, student.LastName);
				if (dbStudent != null)
					return dbStudent;

				dbStudent = context.Students.Add(student);
				await context.SaveChangesAsync();
				return dbStudent;
			}
		}

		public Task<Student> GetStudent(int id)
		{
			using (var context = _universityDbFactory.GetDbContext())
			{
				return context.Students.FirstOrDefaultAsync(s => s.Id == id);
			}
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
			using (var context = _universityDbFactory.GetDbContext())
			{
				return context.Students.FirstOrDefaultAsync(s =>
					s.FirstName.Equals(firstName, StringComparison.InvariantCultureIgnoreCase) &&
					s.LastName.Equals(lastName, StringComparison.InvariantCultureIgnoreCase));
			}
		}
	}
}