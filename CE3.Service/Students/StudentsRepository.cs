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
            		using (var context = _universityDbFactory.GetDbContext())
           		 {
              		  var response = context.Students.Find(id);
             		  response.LastName = newLastName;
           		  await context.SaveChangesAsync();
            		  return response;
            		 }
        	}

		public async Task<Student> CreateStudent(Student student)
		{
            		using (var context = _universityDbFactory.GetDbContext())
            		{
                		var response = context.Students.Add(student);
                		await context.SaveChangesAsync();
                		return response;
            		}
		}

		public async Task<Student> GetStudent(int id)
		{
            		using (var context = _universityDbFactory.GetDbContext())
            		{
                		return await context.Students.FindAsync(id);
            		}
		}

		public async Task<Student> GetStudent(string firstName, string lastName)
		{
            		using (var context = _universityDbFactory.GetDbContext())
            		{
                		return await context.Students.AsNoTracking().FirstOrDefaultAsync(stud=>stud.firstName == firstName && stud.LastName==lastName);
            		}
        	}
	}
}
