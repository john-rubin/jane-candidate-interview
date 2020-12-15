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

		public async Task<Student> ChangeLastName(int id, string newLastName)
		{
			var student = await GetStudent(id);

			if(student != null && !string.IsNullOrEmpty(newLastName))
            {
				using (var db = _universityDbFactory.GetDbContext())
				{
					db.Students.Attach(student);
					student.LastName = newLastName;
					await db.SaveChangesAsync();
				}
			}

			return student;
		}

		public async Task<Student> CreateStudent(Student student)
		{
			Student savedStudent = null;
			using (var db = _universityDbFactory.GetDbContext())
            {
				savedStudent = db.Students.Add(student);
				await db.SaveChangesAsync();
				return savedStudent;
			}

		}

		public async Task<Student> GetStudent(int id)
		{
			using (var db = _universityDbFactory.GetDbContext())
			{
				return await db.Students.FindAsync(id);
			}
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
			throw new NotImplementedException();
		}
	}
}