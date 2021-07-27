
using Doing.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Doing.API.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Home_controller_get_request_should_return_the_content()
        {
            var controller = new HomeController();

            var result = controller.Get();

            var contentResult = result as ContentResult;

            contentResult.Should().NotBeNull();

            contentResult.Content.Should().BeEquivalentTo("Signal from Doing API");
        } 
    }
}