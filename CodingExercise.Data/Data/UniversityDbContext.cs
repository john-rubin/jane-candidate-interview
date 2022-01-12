namespace CodingExercise.Data
{
	public class UniversityDbContext : DbContext, IUniversityDbContext
	{
		public UniversityDbContext() { }
		/// <summary>
		/// Constructor
		/// </summary>
		public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

		public DbSet<Student> Students { get; set; }

		public Task<int> SaveChangesAsync() => SaveChangesAsync();
	}
}