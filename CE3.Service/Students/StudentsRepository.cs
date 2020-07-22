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
			using (var dbc = _universityDbFactory.GetDbContext())
			{
				var stud = (await dbc.Students.FirstOrDefaultAsync(s => s.Id == id));
				if (stud != null) { stud.LastName = newLastName; }
				await dbc.SaveChangesAsync();

				return stud;
			}
		}

		public async Task<Student> CreateStudent(Student student)
		{
			using (var dbc = _universityDbFactory.GetDbContext())
            {
				var stud = dbc.Students.Add(student);
				await dbc.SaveChangesAsync();

				return stud;
			}
		}

		public async Task<Student> GetStudent(int id)
		{
			using (var dbc = _universityDbFactory.GetDbContext())
            {
				var stud = await dbc.Students.FirstOrDefaultAsync(s => s.Id == id);

				return stud;
            }
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
			throw new NotImplementedException();
		}
	}
}