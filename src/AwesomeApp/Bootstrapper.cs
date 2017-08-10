using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using AwesomeApp.service;

namespace AwesomeApp
{
    public class Bootstrapper
    {
        public static void Init(HttpConfiguration configuration)
        {
            RegisterDependencies(configuration);
            RegisterRoutes(configuration);
        }

        static void RegisterDependencies(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DependencyService>().InstancePerLifetimeScope();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        static void RegisterRoutes(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("get message",
                "message",
                new
                {
                    controller = "Message",
                    action = "Get"
                },
                new
                {
                    httpMethod = new HttpMethodConstraint(HttpMethod.Get)
                });
        }
    }
}