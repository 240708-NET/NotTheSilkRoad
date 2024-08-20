using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Services;
using Repository;
using Models;

namespace Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IRepository<Order>> _repoMock;
        private readonly Mock<ILogger<Order>> _loggerMock;
        private readonly OrderServices _orderService;

        public OrderServiceTests()
        {
            _repoMock = new Mock<IRepository<Order>>();
            _loggerMock = new Mock<ILogger<Order>>();
            _orderService = new OrderServices(_repoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            List<Order> expectedOrders = new List<Order>
            {
                new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"},
                new Order {Id = 2, Customer = new Customer{Name = "Jane", Email = "Jane.Doe@Revature.net", Password = "NotStrongPassword456", Address = "345 2nd Ave. New York, NY"}, ShippingAddress = "345 2nd Ave. New York, New York"},
                new Order {Id = 3, Customer = new Customer{Name = "Dave", Email = "Dave.Davidson@Revature.net", Password = "MyNameIsDave789", Address = "678 Washington Dr. Chicago, IL"}, ShippingAddress = "678 Washington Dr. Chicago, IL"}
            };
            _repoMock.Setup(x => x.List()).Returns(expectedOrders);

            // Act
            var result = _orderService.GetAll();

            // Assert
            Assert.Equal(expectedOrders, result);
        }

        [Fact]
        public void GetAll_WhenExceptionThrown_ShouldLogErrorAndReturnEmptyList()
        {
            // Arrange
            _repoMock.Setup(repo => repo.List()).Throws(new Exception("Test exception"));

            // Act
            var result = _orderService.GetAll();

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
            Order expectedOrder = new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};
            _repoMock.Setup(x => x.GetById(1)).Returns(expectedOrder);

            // Act
            var result = _orderService.GetById(1);

            // Assert
            Assert.Equal(expectedOrder, result);
        }

        [Fact]
        public void GetById_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Test exception"));

            // Act
            var result = _orderService.GetById(1);

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
            Order newOrder = new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};
            _repoMock.Setup(x => x.Save(newOrder)).Returns(newOrder);

            // Act
            var result = _orderService.Save(newOrder);

            // Assert
            Assert.Equal(newOrder, result);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Order>())).Throws(new Exception("Test exception"));

            // Act
            var result = _orderService.Save(new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"});

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
            Order orderToDelete = new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};

            // Act
            _orderService.Delete(orderToDelete);

            // Assert
            _repoMock.Verify(x => x.Delete(orderToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            Order orderToDelete = new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Order>())).Throws(new Exception("Test exception"));

            // Act
            _orderService.Delete(orderToDelete);

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
            Order updatedOrder = new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};
            _repoMock.Setup(x => x.Update(1, updatedOrder)).Returns(updatedOrder);

            // Act
            var result = _orderService.Update(1, updatedOrder);

            // Assert
            Assert.Equal(updatedOrder, result);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            Order orderToUpdate = new Order {Id = 1, Customer = new Customer{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};
            _repoMock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Order>())).Throws(new Exception("Test exception"));

            // Act
            var result = _orderService.Update(1, orderToUpdate);

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