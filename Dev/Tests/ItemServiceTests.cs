using Moq;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Services;
using Repository;
using Models;
using DTO;

namespace Tests
{
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _repoMock;
        private readonly Mock<IProductRepository> _repoProductMock;
        private readonly Mock<ILogger<Item>> _loggerMock;
        private readonly ItemServices _itemService;

        public ItemServiceTests()
        {
            _repoMock = new Mock<IItemRepository>();
            _repoProductMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<Item>>();
            _itemService = new ItemServices(_repoMock.Object, _repoProductMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            List<Item> expectedItems = new List<Item>
            {
                new Item {Id = 1, Product = new Product{Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<Category>()}, Quantity = 10, Price = 12.50M},
                new Item {Id = 2, Product = new Product{Id = 2, Title = "Product 2", Description = "This is test product 2.", Price = 7.03M, Categories = new List<Category>()}, Quantity = 3, Price = 21.09M},
                new Item {Id = 3, Product = new Product{Id = 3, Title = "Product 3", Description = "This is test product 3.", Price = 2.00M, Categories = new List<Category>()}, Quantity = 12, Price = 24.00M}
            };
            _repoMock.Setup(x => x.List()).Returns(expectedItems);
            List<ItemDTO> expectedDTOs = expectedItems.Select(i => new ItemDTO(i)).ToList();

            // Act
            var result = _itemService.GetAll();

            // Assert
            result.Should().BeEquivalentTo(expectedDTOs);
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
            Item expectedItem = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<Category>() }, Quantity = 10, Price = 12.50M };
            _repoMock.Setup(x => x.GetById(1)).Returns(expectedItem);
            ItemDTO expectedDTO = new ItemDTO(expectedItem);

            // Act
            var result = _itemService.GetById(1);

            // Assert
            result.Should().BeEquivalentTo(expectedDTO);
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
            Item newItem = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<Category>() }, Quantity = 10, Price = 12.50M };
            ItemDTO newItemDTO = new ItemDTO(newItem);

            _repoMock.Setup(x => x.Save(It.IsAny<Item>())).Returns(newItem);

            // Act
            var result = _itemService.Save(newItemDTO);

            // Assert
            result.Should().BeEquivalentTo(newItemDTO);
        }


        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Item>())).Throws(new Exception("Test exception"));

            // Act
            var result = _itemService.Save(new ItemDTO { Id = 1, Product = new ProductDTO { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<CategoryDTO>() }, Quantity = 10, Price = 12.50M });

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
            Item itemToDelete = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<Category>() }, Quantity = 10, Price = 12.50M };
            ItemDTO itemToDeleteDTO = new ItemDTO(itemToDelete);

            _repoMock.Setup(repo => repo.GetById(itemToDeleteDTO.Id)).Returns(itemToDelete);

            // Act
            _itemService.Delete(itemToDeleteDTO);

            // Assert
            _repoMock.Verify(x => x.Delete(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            ItemDTO itemToDeleteDTO = new ItemDTO { Id = 1, Product = new ProductDTO { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<CategoryDTO>() }, Quantity = 10, Price = 12.50M };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Item>())).Throws(new Exception("Test exception"));

            // Act
            _itemService.Delete(itemToDeleteDTO);

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
            Item updatedItem = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<Category>() }, Quantity = 10, Price = 12.50M };
            ItemDTO updatedItemDTO = new ItemDTO(updatedItem);

            _repoMock.Setup(x => x.Update(It.IsAny<Item>())).Returns(updatedItem);

            // Act
            var result = _itemService.Update(1, updatedItemDTO);

            // Assert
            result.Should().BeEquivalentTo(updatedItemDTO);
        }


        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            Item itemToUpdate = new Item { Id = 1, Product = new Product { Id = 1, Title = "Product 1", Description = "This is test product 1.", Price = 1.25M, Categories = new List<Category>() }, Quantity = 10, Price = 12.50M };
            ItemDTO updatedItemDTO = new ItemDTO(itemToUpdate);

            _repoMock.Setup(repo => repo.Update(It.IsAny<Item>())).Throws(new Exception("Test exception"));

            // Act
            var result = _itemService.Update(1, updatedItemDTO);

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
