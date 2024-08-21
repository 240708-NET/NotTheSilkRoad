using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Services;
using Repository;
using Models;
using DTO;

namespace Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IRepository<Order>> _repoMock;
        private readonly Mock<IRepository<Customer>> _repoCustomerMock;
        private readonly Mock<IRepository<Item>> _repoItemMock;
        private readonly Mock<ILogger<Order>> _loggerMock;
        private readonly OrderServices _orderService;

        public OrderServiceTests()
        {
            _repoMock = new Mock<IRepository<Order>>();
            _repoCustomerMock = new Mock<IRepository<Customer>>();
            _repoItemMock = new Mock<IRepository<Item>>();
            _loggerMock = new Mock<ILogger<Order>>();
            _orderService = new OrderServices(_repoMock.Object, _repoCustomerMock.Object, _repoItemMock.Object, _loggerMock.Object);
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
            List<OrderDTO> expectedDTOs = expectedOrders.Select(i => new OrderDTO(i)).ToList();

            // Act
            var result = _orderService.GetAll();

            // Assert
            Assert.Equal(expectedDTOs, result);
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
            OrderDTO expectedDTO = new OrderDTO(expectedOrder);

            // Act
            var result = _orderService.GetById(1);

            // Assert
            Assert.Equal(expectedDTO, result);
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

            OrderDTO newOrderDTO = new OrderDTO(newOrder);

            // Act
            var result = _orderService.Save(newOrderDTO);

            // Assert
            Assert.Equal(newOrderDTO, result);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Order>())).Throws(new Exception("Test exception"));

            // Act
            var result = _orderService.Save(new OrderDTO {Id = 1, Customer = new CustomerDTO{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"});

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
            OrderDTO orderToDeleteDTO = new OrderDTO(orderToDelete);

            _repoMock.Setup(repo => repo.GetById(orderToDeleteDTO.Id)).Returns(orderToDelete);

            // Act
            _orderService.Delete(orderToDeleteDTO);

            // Assert
            _repoMock.Verify(x => x.Delete(orderToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            OrderDTO orderToDeleteDTO = new OrderDTO {Id = 1, Customer = new CustomerDTO{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Order>())).Throws(new Exception("Test exception"));

            // Act
            _orderService.Delete(orderToDeleteDTO);

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
            OrderDTO updatedOrderDTO = new OrderDTO(updatedOrder);
            
            _repoMock.Setup(x => x.Update(It.IsAny<Order>())).Returns(updatedOrder);

            // Act
            var result = _orderService.Update(1, updatedOrderDTO);

            // Assert
            Assert.Equal(updatedOrderDTO, result);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            OrderDTO orderToUpdateDTO = new OrderDTO {Id = 1, Customer = new CustomerDTO{Name = "John", Email = "John.Doe@Revature.net", Password = "StrongPassword123", Address = "123 Main St. Reston, VA"}, ShippingAddress = "123 Main St. Reston, VA"};
            _repoMock.Setup(repo => repo.Update(It.IsAny<Order>())).Throws(new Exception("Test exception"));

            // Act
            var result = _orderService.Update(1, orderToUpdateDTO);

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