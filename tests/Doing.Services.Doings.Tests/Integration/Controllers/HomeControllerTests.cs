using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Doing.Services.Doings.Tests.Integration.Controllers
{
    public class HomeControllerTests
    {
        private readonly TestServer _server;

        private readonly HttpClient _client;

        public HomeControllerTests()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Home_controller_get_request_should_return_string_content()
        {
            var response = await _client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            content.Should().BeEquivalentTo("Signal from Doing API");
        }

    }
}
