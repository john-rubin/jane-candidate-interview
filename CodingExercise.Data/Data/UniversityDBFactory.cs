namespace CodingExercise.Data
{
	public interface IUniversityDbFactory
	{
		IUniversityDbContext GetDbContext();
	}
	
	public class UniversityDbFactory : IUniversityDbFactory
	{
		public IUniversityDbContext GetDbContext()
		{
			return new UniversityDbContext();
		}
	}
}