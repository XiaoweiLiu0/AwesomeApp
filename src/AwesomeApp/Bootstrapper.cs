using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using AwesomeApp.filters;
using AwesomeApp.service;

namespace AwesomeApp
{
    public class Bootstrapper
    {
        public static ContainerBuilder Init(HttpConfiguration configuration)
        {
            RegisterRoutes(configuration);
            return RegisterModules(configuration);
        }

        public static ContainerBuilder RegisterModules(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DependencyService>().InstancePerLifetimeScope();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<MyLogger>().As<IMyLogger>().InstancePerLifetimeScope();
            configuration.Filters.Add(new LogFilter());

            return builder;
        }

        public static void Finalize(HttpConfiguration configuration, ContainerBuilder builder)
        {
            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static void RegisterRoutes(HttpConfiguration configuration)
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