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
            using (var dbContext = _universityDbFactory.GetDbContext())
            {
                var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (student == null)
                {
                    return null;
                }

                student.LastName = newLastName;
                await dbContext.SaveChangesAsync();

                return student;
            }
		}

		public async Task<Student> CreateStudent(Student student)
        {
            using (var dbContext = _universityDbFactory.GetDbContext())
            {
                if (student == null)
                    return null;

                dbContext.Students.Add(student);
                await dbContext.SaveChangesAsync();

                return student;
			}
        }

		public async Task<Student> GetStudent(int id)
		{
            using (var dbContext = _universityDbFactory.GetDbContext())
            {
                var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
				return student;
            }
		}

		public async Task<Student> GetStudent(string firstName, string lastName)
		{
            using (var dbContext = _universityDbFactory.GetDbContext())
            {
                var student = await dbContext.Students.FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);
                return student;
            }
		}
	}
}