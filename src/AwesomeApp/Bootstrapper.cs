using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using AwesomeApp.filters;
using AwesomeApp.handlers;
using AwesomeApp.service;

namespace AwesomeApp
{
    public class Bootstrapper
    {
        public static void Init(HttpConfiguration configuration, Action<ContainerBuilder> build = null)
        {
            RegisterRoutes(configuration);
            var builder = RegisterModules(configuration, build);

            Finalize(configuration, builder);
        }

        public static ContainerBuilder RegisterModules(HttpConfiguration configuration, Action<ContainerBuilder> build = null)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DependencyService>().InstancePerLifetimeScope();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MyLogger>().As<IMyLogger>().InstancePerLifetimeScope();
//            configuration.Filters.Add(new LogFilter());
            configuration.MessageHandlers.Add(new LogHandler());

            build?.Invoke(builder);

            return builder;
        }

        static void Finalize(HttpConfiguration configuration, ContainerBuilder builder)
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