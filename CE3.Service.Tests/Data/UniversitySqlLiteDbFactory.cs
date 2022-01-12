using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CE3.Service.Data;

namespace CodingExercise.Service.Tests.Data
{
	public class UniversitySqlLiteDbFactory : IUniversityDbFactory
	{
		public IUniversityDbContext GetDbContext()
		{
			return new UniversityDbContext(Effort.DbConnectionFactory.CreatePersistent(RandomNumberGenerator.Create().ToString()));
		}
	}
}
