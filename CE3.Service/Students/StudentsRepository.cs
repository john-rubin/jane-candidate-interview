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
				var foundStudent = await dbContext.Students.SingleOrDefaultAsync(student => student.Id == id);
				if (foundStudent != null)
				{
					foundStudent.LastName = newLastName;
					await dbContext.SaveChangesAsync();
				}
				return foundStudent;
			}
		}

		public async Task<Student> CreateStudent(Student student)
		{
			using (var dbContext = _universityDbFactory.GetDbContext())
			{
				var addedStudent = dbContext.Students.Add(student);
				await dbContext.SaveChangesAsync();
				return addedStudent;
			}
		}

		public Task<Student> GetStudent(int id)
		{
			return DoQuery(async dbContext =>
			{
				var foundStudent = await dbContext.Students.SingleOrDefaultAsync(student => student.Id == id);
				return foundStudent;
			});
			//using (var dbContext = _universityDbFactory.GetDbContext())
			//{
			//	var foundStudent = await dbContext.Students.SingleOrDefaultAsync(student => student.Id == id);
			//	return foundStudent;
			//}
		}

		private async Task<Student> DoQuery(Func<IUniversityDbContext, Task<Student>> query)
		{
			using (var dbContext = _universityDbFactory.GetDbContext())
			{
				var student = await query(dbContext);
				return student;
			}
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
			throw new NotImplementedException();
		}
	}
}