using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using CE3.Controllers;
using CE3.Service.Data;
using CE3.Service.Students;

namespace CE3
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => new StudentsController(c.Resolve<IStudentsService>())).AsSelf().InstancePerLifetimeScope();
            builder.Register(c => new StudentsService(c.Resolve<IStudentsRepository>())).As<IStudentsService>().InstancePerLifetimeScope();
            builder.Register(c => new StudentsRepository(c.Resolve<IUniversityDbFactory>())).As<IStudentsRepository>().InstancePerLifetimeScope();
            builder.Register(c => new UniversityDbFactory()).As<IUniversityDbFactory>().InstancePerLifetimeScope();
            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}