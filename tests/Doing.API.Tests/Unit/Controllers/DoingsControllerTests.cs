using Doing.API.Controllers;
using Doing.API.Repositories;
using Doing.Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Doing.API.Tests.Unit.Controllers
{
    public class DoingsControllerTests
    {
        [Fact]
        public async Task Doings_controller_post_request_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();

            var doingRepositoryMock = new Mock<IDoingRepository>();

            var controller = new DoingsController(busClientMock.Object, doingRepositoryMock.Object);

            var userId = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim( ClaimTypes.Name, userId.ToString()) }
                            , "test"
                        )
                    )
                }
            };

            var command = new CreateDoing
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await controller.Post(command);

            var contentResult = result as AcceptedResult;

            contentResult.Should().NotBeNull();

            contentResult.Location.Should().BeEquivalentTo($"doings/{command.Id} published");
        }
    }
}
