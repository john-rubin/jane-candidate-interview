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
            //throw new NotImplementedException();
            var existingStudent = _studentsRepository.GetStudent(firstName, originalLastName);
            var res = _studentsRepository.ChangeLastName(existingStudent.Id, newLastName);
            return res;
		}

		public Task<Student> CreateStudent(Student student)
		{
            //throw new NotImplementedException();
            return _studentsRepository.CreateStudent(student);
		}

		public Task<Student> GetStudent(int id)
		{
            //throw new NotImplementedException();
            return _studentsRepository.GetStudent(id);
		}

		public Task<Student> GetStudent(string firstName, string lastName)
		{
            //throw new NotImplementedException();
            return _studentsRepository.GetStudent(firstName, lastName);
		}
	}
}