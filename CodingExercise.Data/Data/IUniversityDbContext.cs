namespace CodingExercise.Data
{
	public interface IUniversityDbContext : IDisposable
	{
		DbSet<Student> Students { get; set; }
		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}