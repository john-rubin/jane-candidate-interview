using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CE3.Service.Data
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