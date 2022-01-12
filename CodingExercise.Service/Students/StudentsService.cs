namespace CodingExercise.Service.Students
{
	public interface IStudentsService
	{
		Task<Student> CreateStudent(Student student);
		Task<Student> GetStudent(int id);
		Task<Student> GetStudent(string firstName, string lastName);
		Task<Student> ChangeLastName(string firstName, string originalLastName, string newLastName);
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