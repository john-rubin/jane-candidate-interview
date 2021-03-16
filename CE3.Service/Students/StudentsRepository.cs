using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CE3.Service.Data;

namespace CE3.Service.Students
{
    public interface IStudentsRepository
    {
        Task<Student> CreateStudent(Student student);
        Task<Student> GetStudent(int id);
        Task<Student> GetStudent(String firstName, String lastName);
        Task<Student> ChangeLastName(int id, String newLastName);
    }

    public class StudentsRepository : IStudentsRepository
    {
        private readonly IUniversityDbFactory _universityDbFactory;
        private readonly IUniversityDbContext _universityDbContext;

        public StudentsRepository(IUniversityDbFactory universityDbFactory)
        {
            _universityDbFactory = universityDbFactory;
            _universityDbContext = _universityDbFactory.GetDbContext();
        }

        public async Task<Student> ChangeLastName(int id, string newLastName)
        {
            using (var context = _universityDbFactory.GetDbContext())
            {
                var dbStudent = await context.Students.FindAsync(id);

                if (dbStudent != default)
                {
                    dbStudent.LastName = newLastName;
                    await context.SaveChangesAsync();

                    return dbStudent;
                }
            }

            return default;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            using (var context = _universityDbFactory.GetDbContext())
            {
                var dbStudent = context.Students.Add(student);
                await context.SaveChangesAsync();
                return dbStudent;
            }
        }

        public async Task<Student> GetStudent(int id)
        {
            using (var context = _universityDbFactory.GetDbContext())
            {
                var dbStudent = await context.Students.FindAsync(id);
                return dbStudent;
            }
        }

        public async Task<Student> GetStudent(string firstName, string lastName)
        {
            using (var context = _universityDbFactory.GetDbContext())
            {
                var student = (
                        from s in context.Students
                        where
                            s.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                            && s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)
                        select s)
                    .FirstOrDefault();

                return student;
            }
        }
    }
}