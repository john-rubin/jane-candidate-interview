namespace CodingExercise.Service.Tests
{
	[TestClass]
	public class StudentsServiceTests
	{
		private IUniversityDbContext _universityDbContext;
		private IStudentsService _studentsService;
		private UniversitySqlLiteDbFactory _universitySqlLiteDbFactory;

		[TestInitialize]
		public void Init()
		{
			_universitySqlLiteDbFactory = new UniversitySqlLiteDbFactory();
			_universityDbContext = _universitySqlLiteDbFactory.GetDbContext();
			_studentsService = new StudentsService(
				new StudentsRepository(_universitySqlLiteDbFactory));
		}

		[TestMethod]
		public async Task CreateStudent_Success()
		{
			var student = await _studentsService.CreateStudent(new Student {FirstName = "Test", LastName = "Student"});
			var addedStudent = _universityDbContext.Students.FirstOrDefault(s => s.Id.Equals(student.Id));
			
			Assert.IsNotNull(addedStudent);
			Assert.AreEqual("Test", addedStudent.FirstName);
			Assert.AreEqual("Student", addedStudent.LastName);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task CreateStudent_DuplicateFails()
		{
			var student1 = await _studentsService.CreateStudent(new Student { FirstName = "Test", LastName = "Student" });
			var student2 = await _studentsService.CreateStudent(new Student { FirstName = "Test", LastName = "Student" });
		}

		[TestMethod]
		public async Task GetStudent_Success()
		{
			_universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "One" });
			_universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "Two" });
			_universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "Three" });
			_universityDbContext.SaveChanges();

			var student = await _studentsService.GetStudent("Student", "Two");

			Assert.IsNotNull(student);
			Assert.AreEqual("Student", student.FirstName);
			Assert.AreEqual("Two", student.LastName);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public async Task GetStudent_StudentNotFoundThrowsException()
		{
			_universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "One" });
			_universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "Two" });
			_universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "Three" });
			_universityDbContext.SaveChanges();

			var student = await _studentsService.GetStudent("Student", "Four");
		}

		[TestMethod]
		public async Task ChangeLastName_Success()
		{
			var student = _universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "Original" }).Entity;
			_universityDbContext.SaveChanges();

			await _studentsService.ChangeLastName(student.FirstName, student.LastName, "New");

			Student studentFromDb = _universityDbContext.Students.AsNoTracking().FirstOrDefault(s =>
				(s.FirstName.Equals("Student") && s.LastName.Equals("New")));
			
			Assert.IsNotNull(studentFromDb);
			Assert.AreEqual("New", studentFromDb.LastName);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task ChangeLastName_CreatesDuplicateThrowsException()
		{
			var student1 = _universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "One" }).Entity;
			var student2 = _universityDbContext.Students.Add(new Student() { FirstName = "Student", LastName = "Two" }).Entity;
			_universityDbContext.SaveChanges();

			await _studentsService.ChangeLastName(student1.FirstName, student1.LastName, "Two");
		}

	}
}
