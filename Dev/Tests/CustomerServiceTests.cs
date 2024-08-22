using Moq;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Services;
using Repository;
using Models;
using DTO;

namespace Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _repoMock;
        private readonly Mock<IOrderRepository> _repoOrderMock;
        private readonly Mock<ILogger<Customer>> _loggerMock;
        private readonly CustomerServices _customerService;

        public CustomerServiceTests()
        {
            _repoMock = new Mock<ICustomerRepository>();
            _repoOrderMock = new Mock<IOrderRepository>();
            _loggerMock = new Mock<ILogger<Customer>>();
            _customerService = new CustomerServices(_repoMock.Object, _repoOrderMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            List<Customer> expectedCustomers = new List<Customer>
            {
                new Customer {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA"},
                new Customer {Id = 2, Name = "Jane Doe", Email = "Jane.Doe@Revature.net", Password = "NotStrondPassword456", Address = "456 Main St. Reston, VA"},
                new Customer {Id = 3, Name = "Dave Davidson", Email = "Dave.Davidson@Revature.net", Password = "MyNameIsDave789", Address = "789 Main St. Reston, VA"}
            };
            _repoMock.Setup(x => x.List()).Returns(expectedCustomers);
            List<CustomerDTO> expectedDTOs = expectedCustomers.Select(i => new CustomerDTO(i, false)).ToList();

            // Act
            var result = _customerService.GetAll();

            // Assert
            result.Should().BeEquivalentTo(expectedDTOs);
        }

        [Fact]
        public void GetAll_WhenExceptionThrown_ShouldLogErrorAndReturnEmptyList()
        {
            // Arrange
            _repoMock.Setup(repo => repo.List()).Throws(new Exception("Test exception"));

            // Act
            var result = _customerService.GetAll();

            // Assert
            Assert.Empty(result);
            _loggerMock.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public void GetById_CorrectOutput()
        {
            // Arrange
            Customer expectedCustomer = new Customer {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA"};
            _repoMock.Setup(x => x.GetById(1)).Returns(expectedCustomer);
            CustomerDTO expectedDTO = new CustomerDTO(expectedCustomer, false);

            // Act
            var result = _customerService.GetById(1);

            // Assert
            result.Should().BeEquivalentTo(expectedDTO);
        }

        [Fact]
        public void GetById_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Test exception"));

            // Act
            var result = _customerService.GetById(1);

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

        [Fact]
        public void Save_CorrectOutput()
        {
            // Arrange
            Customer newCustomer = new Customer {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA"};
            _repoMock.Setup(x => x.Save(It.IsAny<Customer>())).Returns(newCustomer);

            CustomerDTO newCustomerDTO = new CustomerDTO(newCustomer, false);

            // Act
            var result = _customerService.Save(newCustomerDTO);

            // Assert
            result.Should().BeEquivalentTo(newCustomerDTO);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Customer>())).Throws(new Exception("Test exception"));

            // Act
            var result = _customerService.Save(new CustomerDTO { Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA" });

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

        [Fact]
        public void Delete_ShouldInvokeRepositoryDelete()
        {
            // Arrange
            Customer customerToDelete = new Customer {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA"};
            CustomerDTO customerToDeleteDTO = new CustomerDTO(customerToDelete, false);

            _repoMock.Setup(repo => repo.GetById(customerToDeleteDTO.Id)).Returns(customerToDelete);

            // Act
            _customerService.Delete(customerToDeleteDTO);

            // Assert
            _repoMock.Verify(x => x.Delete(customerToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            CustomerDTO customerToDeleteDTO = new CustomerDTO { Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA"};
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Customer>())).Throws(new Exception("Test exception"));

            // Act
            _customerService.Delete(customerToDeleteDTO);

            // Assert
            _loggerMock.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public void Update_CorrectOutput()
        {
            // Arrange
            Customer updatedCustomer = new Customer {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA"};
            CustomerDTO updatedCustomerDTO = new CustomerDTO(updatedCustomer, false);
            
            _repoMock.Setup(x => x.Update(It.IsAny<Customer>())).Returns(updatedCustomer);

            // Act
            var result = _customerService.Update(1, updatedCustomerDTO);

            // Assert
            result.Should().BeEquivalentTo(updatedCustomerDTO);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            CustomerDTO customerToUpdateDTO = new CustomerDTO {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Address = "123 Main St. Reston, VA"};
            _repoMock.Setup(repo => repo.Update(It.IsAny<Customer>())).Throws(new Exception("Test exception"));

            // Act
            var result = _customerService.Update(1, customerToUpdateDTO);

            // Assert
            Assert.Null(result);
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Test exception")),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}