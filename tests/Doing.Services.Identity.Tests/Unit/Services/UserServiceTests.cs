using Doing.Common.Auth;
using Doing.Services.Identity.Domain.Models;
using Doing.Services.Identity.Domain.Repositories;
using Doing.Services.Identity.Domain.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Doing.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task User_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "secret";
            var name = "test";
            var salt = "salt";
            var hash = "hash";
            var token = "token";

            var userRepositoryMock = new Mock<IUserRepository>();

            var encrypterMock = new Mock<IEncrypter>();

            var jwtHandlerMock = new Mock<IJwtHandler>();

            encrypterMock.Setup(x => x.GetSalt()).Returns(salt);

            encrypterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);

            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken
            {
                Token = token
            });

            var user = new User(email, name);

            user.SetPassword(password, encrypterMock.Object);

            userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object,
                encrypterMock.Object, jwtHandlerMock.Object);

            var jwt = await userService.LogInAsync(email, password);

            userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);

            jwtHandlerMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);

            jwt.Should().NotBeNull();

            jwt.Token.Should().BeEquivalentTo(token);
        }
    }
}
