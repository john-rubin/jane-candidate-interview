using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;

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
