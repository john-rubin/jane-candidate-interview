namespace CodingExercise.Data.Entities
{
	[Table("students")]
	public class Student
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}