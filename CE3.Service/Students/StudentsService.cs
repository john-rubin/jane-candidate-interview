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

		public async Task<Student> ChangeLastName(string firstName, string originalLastName, string newLastName)
		{
			var studentToChange = await _studentsRepository.GetStudent(firstName, originalLastName);
			if(studentToChange != null)
            {
				var student = await _studentsRepository.ChangeLastName(studentToChange.Id, newLastName);
				return student;
			}
			return null;
		}

		public async Task<Student> CreateStudent(Student student)
		{
			return await _studentsRepository.CreateStudent(student);
		}

		public async Task<Student> GetStudent(int id)
		{
			var student = await _studentsRepository.GetStudent(id);
			if(student == null)
            {
				throw new NotFoundException("Student(id) not found");
            }
			return student;
		}

		public async Task<Student> GetStudent(string firstName, string lastName)
		{
			var student = await _studentsRepository.GetStudent(firstName, lastName);
			if (student == null)
			{
				throw new NotFoundException("Student(first,last) not found");
			}
			return student;
		}
	}
}