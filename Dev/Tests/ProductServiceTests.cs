using Moq;
using Models;
using Services;
using Repository;
using Microsoft.Extensions.Logging;
using DTO;
using FluentAssertions;

namespace Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repoMock;
        private readonly Mock<ISellerRepository> _repoSellerMock;
        private readonly Mock<ICategoryRepository> _repoCategoryMock;
        private readonly Mock<ILogger<Product>> _loggerMock;
        private readonly ProductServices _productService;

        public ProductServiceTests()
        {
            _loggerMock = new Mock<ILogger<Product>>();
            _repoMock = new Mock<IProductRepository>();
            _repoSellerMock = new Mock<ISellerRepository>();
            _repoCategoryMock = new Mock<ICategoryRepository>();
            _productService = new ProductServices(_repoMock.Object, _repoSellerMock.Object, _repoCategoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            List<Product> expectedProducts = new List<Product>
            {
                new Product { Id = 1, Title = "Name 1", Description = "Product 1", Price = 1.00M },
                new Product { Id = 2, Title = "Name 2", Description = "Product 2", Price = 2.00M },
                new Product { Id = 3, Title = "Name 3", Description = "Product 3", Price = 3.00M }
            };
            _repoMock.Setup(x => x.List()).Returns(expectedProducts);
            List<ProductDTO> expectedDTOs = expectedProducts.Select(i => new ProductDTO(i, true)).ToList();

            // Act
            var result = _productService.GetAll();
            
            // Assert
            result.Should().BeEquivalentTo(expectedDTOs);
        }

        [Fact]
        public void GetAll_WhenExceptionThrown_ShouldLogErrorAndReturnEmptyList()
        {
            // Arrange
            _repoMock.Setup(repo => repo.List()).Throws(new Exception("Test exception"));

            // Act
            var result = _productService.GetAll();

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
            Product expectedProduct = new Product { Id = 1, Title = "Name 1", Description = "Product 1", Price = 2.50M };
            _repoMock.Setup(x => x.GetById(1)).Returns(expectedProduct);
            ProductDTO expectedDTO = new ProductDTO(expectedProduct, true);

            // Act
            var result = _productService.GetById(1);

            // Assert
            result.Should().BeEquivalentTo(expectedDTO);
        }

        [Fact]
        public void GetById_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Test exception"));

            // Act
            var result = _productService.GetById(1);

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
        public void GetBySellerId_CorrectOutput()
        {
            // Arrange
            List<Product> expectedProducts = new List<Product>
            {
                new Product { Id = 1, Title = "Name 1", Description = "Product 1", Price = 1.00M, Seller = new Seller { Id = 1, Name = "Seller 1", Email = "seller1@rev.net", Password = "sellerPass1" }},
                new Product { Id = 2, Title = "Name 2", Description = "Product 2", Price = 2.00M, Seller = new Seller { Id = 1, Name = "Seller 1", Email = "seller1@rev.net", Password = "sellerPass1" }},
                new Product { Id = 3, Title = "Name 3", Description = "Product 3", Price = 3.00M, Seller = new Seller { Id = 2, Name = "Seller 2", Email = "seller2@rev.net", Password = "sellerPass2" }}
            };
            _repoMock.Setup(x => x.GetBySellerId(1)).Returns(expectedProducts);
            List<ProductDTO> expectedDTOs = expectedProducts.Select(i => new ProductDTO(i, true)).ToList();

            // Act
            var result = _productService.GetBySellerId(1);
            
            // Assert
            result.Should().BeEquivalentTo(expectedDTOs);
        }

        [Fact]
        public void GetBySellerId_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetBySellerId(It.IsAny<int>())).Throws(new Exception("Test exception"));

            // Act
            var result = _productService.GetBySellerId(1);

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
            Product newProduct = new Product { Id = 1, Title = "Name 1", Description = "New Product", Price = 3.25M };
            _repoMock.Setup(x => x.Save(It.IsAny<Product>())).Returns(newProduct);

            ProductDTO newProductDTO = new ProductDTO(newProduct, true);

            // Act
            var result = _productService.Save(newProductDTO);

            // Assert
            result.Should().BeEquivalentTo(newProductDTO);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Product>())).Throws(new Exception("Test exception"));

            // Act
            var result = _productService.Save(new ProductDTO { Id = 1, Title = "Name 1", Description = "Test", Price = 0.01M });

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
            Product productToDelete = new Product { Id = 1, Title = "Name 1", Description = "Product to delete", Price = 1.11M };
            ProductDTO productToDeleteDTO = new ProductDTO(productToDelete, true);

            _repoMock.Setup(repo => repo.GetById(productToDeleteDTO.Id)).Returns(productToDelete);

            // Act
            _productService.Delete(productToDeleteDTO);

            // Assert
            _repoMock.Verify(x => x.Delete(productToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            ProductDTO productToDeleteDTO = new ProductDTO { Id = 1, Title = "Name 1", Description = "Product to delete", Price = 2.50M};
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Product>())).Throws(new Exception("Test exception"));

            // Act
            _productService.Delete(productToDeleteDTO);

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
            Product updatedProduct = new Product { Id = 1, Title = "Name 1", Description = "Updated Product", Price = 25.99M };
            ProductDTO updatedProductDTO = new ProductDTO(updatedProduct, true);
            
            _repoMock.Setup(x => x.Update(It.IsAny<Product>())).Returns(updatedProduct);

            // Act
            var result = _productService.Update(1, updatedProductDTO);

            // Assert
            result.Should().BeEquivalentTo(updatedProductDTO);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            ProductDTO productToUpdateDTO = new ProductDTO { Id = 1, Title = "Name 1", Description = "Updated Product", Price = 30.00M };
            _repoMock.Setup(repo => repo.Update(It.IsAny<Product>())).Throws(new Exception("Test exception"));

            // Act
            var result = _productService.Update(1, productToUpdateDTO);

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
