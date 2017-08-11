using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;

namespace AwesomeApp.Test
{
    public class ApiTestBase : IDisposable
    {
        HttpConfiguration configuration;
        HttpServer server;
        protected HttpClient client;
        ContainerBuilder builder;

        public ApiTestBase()
        {
            configuration = new HttpConfiguration();
            builder = Bootstrapper.Init(configuration);
        }

        public void Init(Action<ContainerBuilder> build = null)
        {
            build?.Invoke(builder);
            Bootstrapper.Finalize(configuration, builder);
            CreateServer();
            CreateClient();
        }

        void CreateClient()
        {
            client = new HttpClient(server)
            {
                BaseAddress = new Uri("http://baidu.com/")
            };
        }

        void CreateServer()
        {
            server = new HttpServer(configuration);
        }

        public void Dispose()
        {
            configuration?.Dispose();
            server?.Dispose();
            client?.Dispose();
        }
    }
}