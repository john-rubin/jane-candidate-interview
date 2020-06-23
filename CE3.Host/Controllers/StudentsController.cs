using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CE3.Models;
using CE3.Service.Students;

namespace CE3.Controllers
{
    public class StudentsController : ApiController
    {
	    private readonly IStudentsService _studentsService;

	    public StudentsController(IStudentsService studentsService)
	    {
		    _studentsService = studentsService;
	    }

		[HttpGet]
		[Route("api/Students")]
		public IEnumerable<string> Get()
        {
	        throw new NotImplementedException();
        }

	    [HttpGet]
	    [Route("api/Students/{id}")]
	    public async Task<Student> Get([FromUri]int id)
	    {
		    return await _studentsService.GetStudent(id);
		}

	    [HttpGet]
	    [Route("api/Students/{firstName}/{lastName}")]
	    public async Task<Student> Get(String firstName, String lastName)
	    {
		    return await _studentsService.GetStudent(firstName, lastName);
	    }


		[HttpPost]
		[Route("api/Students")]
		public Task<Student> CreateStudent(CreateStudentRequest request)
		{
			return _studentsService.CreateStudent(
				new Student
				{
					FirstName = request.FirstName, LastName = request.LastName
				});
		}

		[HttpPut]
		[Route("api/Students")]
		public Task<Student> Put(ChangeStudentLastNameRequest request)
		{
			return _studentsService.ChangeLastName(request.FirstName, request.OriginalLastName, request.NewLastName);
		}


	}
}
