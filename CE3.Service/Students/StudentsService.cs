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
			 var response = await _studentsRepository.GetStudent(firstName, originalLastName);
            		return await _studentsRepository.ChangeLastName(response.Id, newLastName);
		}

		public Task<Student> CreateStudent(Student student)
		{
			return await _studentsRepository.CreateStudent(student);
		}

		public Task<Student> GetStudent(int id)
		{
			return await _studentsRepository.GetStudent(id);
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
			return await _studentsRepository.GetStudent(id);
		}
	}
}
