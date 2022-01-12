using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Tests.Data
{
	public class UniversitySqlLiteDbFactory : IUniversityDbFactory
	{
		public IUniversityDbContext GetDbContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<UniversityDbContext>();
			optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString("N")); //different lookup to allow for simultaneous test runs with individual seeding

			return new UniversityDbContext(optionsBuilder.Options);
		}
	}
}
