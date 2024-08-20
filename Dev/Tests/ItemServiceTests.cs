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
    public class ItemServiceTests
    {
        private readonly Mock<IRepository<Item>> _repoMock;
        private readonly Mock<ILogger<Item>> _loggerMock;
        private readonly ItemServices _itemService;

        public ItemServiceTests()
        {
            _repoMock = new Mock<IRepository<Item>>();
            _loggerMock = new Mock<ILogger<Item>>();
            _itemService = new ItemServices(_repoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            List<Item> expectedItems = new List<Item>
            {
                new Item {Id = 1, Product = new Product{Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>()}, Quantity = 10, Price = 12.50F},
                new Item {Id = 2, Product = new Product{Id = 2, Title = "Product 2", Description = "This is test product 2.", Price = 7.03F, Categories = new List<Category>()}, Quantity = 3, Price = 21.09F},
                new Item {Id = 3, Product = new Product{Id = 3, Title = "Product 3", Description = "This is test product 3.", Price = 2.00F, Categories = new List<Category>()}, Quantity = 12, Price = 24.00F}
            };
            _repoMock.Setup(x => x.List()).Returns(expectedItems);

            // Act
            var result = _itemService.GetAll();

            // Assert
            Assert.Equal(expectedItems, result);
        }

        [Fact]
        public void GetAll_WhenExceptionThrown_ShouldLogErrorAndReturnEmptyList()
        {
            // Arrange
            _repoMock.Setup(repo => repo.List()).Throws(new Exception("Test exception"));

            // Act
            var result = _itemService.GetAll();

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
            Item expectedItem = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>() }, Quantity = 10, Price = 12.50F };
            _repoMock.Setup(x => x.GetById(1)).Returns(expectedItem);

            // Act
            var result = _itemService.GetById(1);

            // Assert
            Assert.Equal(expectedItem, result);
        }

        [Fact]
        public void GetById_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Test exception"));

            // Act
            var result = _itemService.GetById(1);

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
            Item newItem = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>() }, Quantity = 10, Price = 12.50F };
            _repoMock.Setup(x => x.Save(newItem)).Returns(newItem);

            // Act
            var result = _itemService.Save(newItem);

            // Assert
            Assert.Equal(newItem, result);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Item>())).Throws(new Exception("Test exception"));

            // Act
            var result = _itemService.Save(new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>() }, Quantity = 10, Price = 12.50F });

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
            Item itemToDelete = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>() }, Quantity = 10, Price = 12.50F };

            // Act
            _itemService.Delete(itemToDelete);

            // Assert
            _repoMock.Verify(x => x.Delete(itemToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            Item itemToDelete = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>() }, Quantity = 10, Price = 12.50F };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Item>())).Throws(new Exception("Test exception"));

            // Act
            _itemService.Delete(itemToDelete);

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
            Item updatedItem = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>() }, Quantity = 10, Price = 12.50F };
            _repoMock.Setup(x => x.Update(1, updatedItem)).Returns(updatedItem);

            // Act
            var result = _itemService.Update(1, updatedItem);

            // Assert
            Assert.Equal(updatedItem, result);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            Item itemToUpdate = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25F, Categories = new List<Category>() }, Quantity = 10, Price = 12.50F };
            _repoMock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Item>())).Throws(new Exception("Test exception"));

            // Act
            var result = _itemService.Update(1, itemToUpdate);

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
