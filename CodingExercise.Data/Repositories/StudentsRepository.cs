namespace CodingExercise.Data.Repositories
{
	public interface IStudentsRepository
	{
		Task<Student> CreateStudent(Student student);
		Task<Student> GetStudent(int id);
		Task<Student> GetStudent(string firstName, string lastName);
		Task<Student> ChangeLastName(int id, string newLastName);

	}

	public class StudentsRepository : IStudentsRepository
	{
		private readonly IUniversityDbFactory _universityDbFactory;

		public StudentsRepository(IUniversityDbFactory universityDbFactory)
		{
			_universityDbFactory = universityDbFactory;
		}

		public Task<Student> ChangeLastName(int id, string newLastName)
		{
			throw new NotImplementedException();
		}

		public async Task<Student> CreateStudent(Student student)
		{
			using var db = _universityDbFactory.GetDbContext();
			var result = db.Students.Add(student);
			await db.SaveChangesAsync();

			return result.Entity;
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