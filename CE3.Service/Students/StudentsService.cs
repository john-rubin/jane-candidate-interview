using System;
using System.Threading.Tasks;
using CE3.Service.Exceptions;

namespace CE3.Service.Students
{
	public interface IStudentsService
	{
		Task<Student> CreateStudent(Student student);
		Task<Student> GetStudent(int id);
		Task<Student> GetStudent(String firstName, String lastName);
		Task<Student> ChangeLastName(String firstName, String originalLastName, String newLastName);
	}

	public class StudentsService : IStudentsService
	{
		private readonly IStudentsRepository _studentsRepository;

		public StudentsService(IStudentsRepository studentsRepository)
		{
			_studentsRepository = studentsRepository;
		}

		public Task<Student> ChangeLastName(string firstName, string originalLastName, string newLastName)
		{
			throw new NotImplementedException();
		}

		public Task<Student> CreateStudent(Student student)
		{
			throw new NotImplementedException();
		}

		public async Task<Student> GetStudent(int id)
		{
			if (id <= 0)
            {
				throw new Exception("Invalid ID Request");
            }


			var student = await _studentsRepository.GetStudent(id);

			return student;
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
			throw new NotImplementedException();
		}
	}
}