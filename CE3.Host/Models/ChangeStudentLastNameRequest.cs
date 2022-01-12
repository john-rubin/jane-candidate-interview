using System;

namespace CodingExercise.Data.Entities
{
	public class ChangeStudentLastNameRequest
	{
		public String FirstName { get; set; }
		public String OriginalLastName { get; set; }
		public String NewLastName { get; set; }
	}
}