namespace CodingExercise.Contracts.Students
{
	public class ChangeStudentLastNameRequest
	{
		public string FirstName { get; set; }
		public string OriginalLastName { get; set; }
		public string NewLastName { get; set; }
	}
}