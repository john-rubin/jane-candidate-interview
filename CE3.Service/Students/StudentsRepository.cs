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
            //throw new NotImplementedException();
            var students = _universityDbFactory.GetDbContext().Students.Include(s => s.Id == id).ToList();
            Student aStudent = students[0];
            aStudent.LastName = newLastName;
            var res =_universityDbFactory.GetDbContext().SaveChanges();
            return aStudent;
		}

		public async Task<Student> CreateStudent(Student student)
		{
            //throw new NotImplementedException();
            var students = await _universityDbFactory.GetDbContext().Students.FirstOrDefaultAsync(s => s.Id == student.Id);
            _universityDbFactory.GetDbContext().Students.Add(student);
            var res =_universityDbFactory.GetDbContext().SaveChanges();
            return student;
		}

		public Task<Student> GetStudent(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
			throw new NotImplementedException();
		}
	}
}