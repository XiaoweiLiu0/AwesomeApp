using System;
using System.Net.Http;
using System.Web.Http;

namespace AwesomeApp.Test
{
    public class ApiTestBase : IDisposable
    {
        HttpConfiguration configuration;
        HttpServer server;
        protected HttpClient client;

        public ApiTestBase()
        {
            CreateConfiguration();
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

        void CreateConfiguration()
        {
            configuration = new HttpConfiguration();
            Bootstrapper.Init(configuration);
        }

        public void Dispose()
        {
            configuration?.Dispose();
            server?.Dispose();
            client?.Dispose();
        }
    }
}