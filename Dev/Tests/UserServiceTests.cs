using Moq;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Services;
using Repository;
using Models;
using DTO;

namespace Tests
{
    public class UserServiceTests
    {
        private readonly Mock<ILogger<Seller>> _loggerMock;
        private readonly Mock<IUserRepository> _repoMock;
        private readonly UserServices _userService;

        public UserServiceTests()
        {
            _repoMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<Seller>>();
            _userService = new UserServices(_repoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetByEmail_CorrectOutput()
        {
            // Arrange
            Customer expectedUser = new Customer { Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = null, Address = "123 Main St. Reston, VA" };
            _repoMock.Setup(x => x.GetByEmail("John.Doe@Revature.net")).Returns(expectedUser);
            CustomerDTO expectedDTO = new CustomerDTO(expectedUser, true);

            // Act
            var result = _userService.GetByEmail("John.Doe@Revature.net");

            // Assert
            result.Should().BeEquivalentTo(expectedDTO);
        }

        [Fact]
        public void GetByEmail_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetByEmail(It.IsAny<string>())).Throws(new Exception("Test exception"));

            // Act
            var result = _userService.GetByEmail("John.Doe@Revature.net");

            // Assert
            Assert.Null(result);
            _loggerMock.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}