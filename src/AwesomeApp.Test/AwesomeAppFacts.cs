using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Autofac;
using AwesomeApp.filters;
using Xunit;

namespace AwesomeApp.Test
{
    public class AwesomeAppFacts : ApiTestBase
    {
        [Fact]
        public async Task should_say_hello()
        {
            Init();
            var response = await client.GetAsync("message");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var message = await response.Content.ReadAsStringAsync();
            Assert.Equal("hello", message);
        }

        [Fact]
        public void should_log_preformance()
        {
            var fakeLogger = new FakeLogger();
            Init(b => b.RegisterInstance(fakeLogger).As<IMyLogger>());
            client.GetAsync("message");

            Assert.Equal(3, fakeLogger.GetLog());
        }

    }

    public class FakeLogger : IMyLogger
    {
        readonly List<string> logStorage = new List<string>();

        public void Log(string line)
        {
            logStorage.Add(line);
        }

        public int GetLog()
        {
            return logStorage.Count;
        }
    }
}
