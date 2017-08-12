using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;

namespace AwesomeApp.Test
{
    public class ApiTestBase : IDisposable
    {
        readonly HttpConfiguration configuration;
        HttpServer server;
        protected HttpClient client;

        public ApiTestBase()
        {
            configuration = new HttpConfiguration();
        }

        public void Init(Action<ContainerBuilder> build = null)
        {
            Bootstrapper.Init(configuration, build);
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