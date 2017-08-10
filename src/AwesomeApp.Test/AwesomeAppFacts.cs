using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AwesomeApp.Test
{
    public class AwesomeAppFacts : ApiTestBase
    {
        [Fact]
        public async Task should_say_hello()
        {
            var response = await client.GetAsync("message");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var message = await response.Content.ReadAsStringAsync();
            Assert.Equal("hello", message);
        }
    }
}
