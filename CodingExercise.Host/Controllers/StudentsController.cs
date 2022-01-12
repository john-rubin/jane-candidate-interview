namespace CodingExercise.Controllers
{
	[Route("api/students")]
    public class StudentsController : ControllerBase
    {
	    private readonly IStudentsService _studentsService;

	    public StudentsController(IStudentsService studentsService)
	    {
		    _studentsService = studentsService;
	    }

		[HttpGet]
		[Route("all")]
		public IEnumerable<string> Get()
        {
	        throw new NotImplementedException();
        }

	    [HttpGet]
	    [Route("{id}")]
	    public async Task<Student> Get([FromQuery]int id)
	    {
		    return await _studentsService.GetStudent(id);
		}

	    [HttpGet]
	    [Route("{firstName}/{lastName}")]
	    public async Task<Student> Get(string firstName, string lastName)
	    {
		    return await _studentsService.GetStudent(firstName, lastName);
	    }


		[HttpPost]
		[Route("")]
		public Task<Student> CreateStudent(CreateStudentRequest request)
		{
			return _studentsService.CreateStudent(
				new Student
				{
					FirstName = request.FirstName, LastName = request.LastName
				});
		}

		[HttpPut]
		[Route("")]
		public Task<Student> Put(ChangeStudentLastNameRequest request)
		{
			return _studentsService.ChangeLastName(request.FirstName, request.OriginalLastName, request.NewLastName);
		}


	}
}
