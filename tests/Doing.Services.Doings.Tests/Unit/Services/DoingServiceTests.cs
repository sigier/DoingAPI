using Doing.Services.Doings.Domain.Models;
using Doing.Services.Doings.Domain.Repositories;
using Doing.Services.Doings.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Doing.Services.Doings.Tests.Unit.Services
{
    public class DoingServiceTests
    {
        [Fact]
        public async Task Doing_service_add_method_should_succeed()
        {
            var category = "test";

            var doingRepositoryMock = new Mock<IDoingRepository>();

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(x => x.GetAsync(category))

                .ReturnsAsync(new CategoryModel(category));

            var doingService = new DoingService(doingRepositoryMock.Object,
                categoryRepositoryMock.Object);

            var id = Guid.NewGuid();

            await doingService.AddAsync(id, Guid.NewGuid(), category, "test",
                "description", DateTime.UtcNow);

            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);

            doingRepositoryMock.Verify(x => x.AddAsync(It.IsAny<DoingModel>()), Times.Once);
        }
    }
}
