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
			using (var dbcontext = _universityDbFactory.GetDbContext())
            {
				var studentToChange = await dbcontext.Students.FindAsync(id);
				if(studentToChange != null)
                {
					var clashingStudent = await dbcontext.Students.FirstOrDefaultAsync<Student>(s => s.FirstName == studentToChange.FirstName && s.LastName == newLastName);
					if(clashingStudent != null)
                    {
						//clash, we cant change to this name or there will be a dupe in the table
						throw new InvalidOperationException("Can't change to this name because a student of this name already exists.");
					}

					studentToChange.LastName = newLastName;
					await dbcontext.SaveChangesAsync();

					return studentToChange;
				}
            }
			return null;
		}

		public async Task<Student> CreateStudent(Student student)
		{
			using (var dbcontext = _universityDbFactory.GetDbContext())
			{
				var checkStudent = await GetStudent(student.FirstName, student.LastName);
				if(checkStudent != null)
                {
					throw new InvalidOperationException("Can't add student that already exists.");
				}
				var myStudent = dbcontext.Students.Add(student);
				await dbcontext.SaveChangesAsync();
				return myStudent;
			}
		}

		public async Task<Student> GetStudent(int id)
		{
            using (var dbcontext = _universityDbFactory.GetDbContext())
            {
                Student student = await dbcontext.Students.FirstOrDefaultAsync<Student>(s => s.Id == id);
                return student;
            }
		}

		public async Task<Student> GetStudent(string firstName, string lastName)
		{
			using (var dbcontext = _universityDbFactory.GetDbContext())
			{
				Student student = await dbcontext.Students.FirstOrDefaultAsync<Student>(s => s.FirstName == firstName && s.LastName == lastName);
				return student;
			}
		}
	}
}