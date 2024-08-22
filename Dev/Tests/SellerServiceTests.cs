using Moq;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Services;
using Repository;
using Models;
using DTO;

namespace Tests
{
    public class SellerServiceTests
    {
        private readonly Mock<ISellerRepository> _repoMock;
        private readonly Mock<IProductRepository> _repoProductMock;
        private readonly Mock<ILogger<Seller>> _loggerMock;
        private readonly SellerServices _sellerService;

        public SellerServiceTests()
        {
            _repoMock = new Mock<ISellerRepository>();
            _repoProductMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<Seller>>();
            _sellerService = new SellerServices(_repoMock.Object, _repoProductMock.Object,_loggerMock.Object);
        }

        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            List<Seller> expectedSellers = new List<Seller>
            {
                new Seller {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<Product>()},
                new Seller {Id = 2, Name = "Jane Doe", Email = "Jane.Doe@Revature.net", Password = "NotStrondPassword456", Products = new List<Product>()},
                new Seller {Id = 3, Name = "Dave Davidson", Email = "Dave.Davidson@Revature.net", Password = "MyNameIsDave789", Products = new List<Product>()}
            };
            _repoMock.Setup(x => x.List()).Returns(expectedSellers);
            List<SellerDTO> expectedDTOs = expectedSellers.Select(i => new SellerDTO(i, false)).ToList();

            // Act
            var result = _sellerService.GetAll();

            // Assert
            result.Should().BeEquivalentTo(expectedDTOs);
        }

        [Fact]
        public void GetAll_WhenExceptionThrown_ShouldLogErrorAndReturnEmptyList()
        {
            // Arrange
            _repoMock.Setup(repo => repo.List()).Throws(new Exception("Test exception"));

            // Act
            var result = _sellerService.GetAll();

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
            Seller expectedSeller = new Seller {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<Product>()};
            _repoMock.Setup(x => x.GetById(1)).Returns(expectedSeller);
            SellerDTO expectedDTO = new SellerDTO(expectedSeller, true);

            // Act
            var result = _sellerService.GetById(1);

            // Assert
            result.Should().BeEquivalentTo(expectedDTO);
        }

        [Fact]
        public void GetById_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Test exception"));

            // Act
            var result = _sellerService.GetById(1);

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
            Seller newSeller = new Seller {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<Product>()};
            _repoMock.Setup(x => x.Save(It.IsAny<Seller>())).Returns(newSeller);

            SellerDTO newSellerDTO = new SellerDTO(newSeller, true);

            // Act
            var result = _sellerService.Save(newSellerDTO);

            // Assert
            result.Should().BeEquivalentTo(newSellerDTO);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Seller>())).Throws(new Exception("Test exception"));

            // Act
            var result = _sellerService.Save(new SellerDTO { Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<ProductDTO>()});

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
            Seller sellerToDelete = new Seller {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<Product>()};
            SellerDTO sellerToDeleteDTO = new SellerDTO(sellerToDelete, true);

            _repoMock.Setup(repo => repo.GetById(sellerToDeleteDTO.Id)).Returns(sellerToDelete);

            // Act
            _sellerService.Delete(sellerToDeleteDTO);

            // Assert
            _repoMock.Verify(x => x.Delete(sellerToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            SellerDTO sellerToDeleteDTO = new SellerDTO {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<ProductDTO>()};
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Seller>())).Throws(new Exception("Test exception"));

            // Act
            _sellerService.Delete(sellerToDeleteDTO);

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
            Seller updatedSeller = new Seller {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<Product>()};
            SellerDTO updatedSellerDTO = new SellerDTO(updatedSeller, true);
            
            _repoMock.Setup(x => x.Update(It.IsAny<Seller>())).Returns(updatedSeller);

            // Act
            var result = _sellerService.Update(1, updatedSellerDTO);

            // Assert
            result.Should().BeEquivalentTo(updatedSellerDTO);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            SellerDTO sellerToUpdateDTO = new SellerDTO {Id = 1, Name = "John Doe", Email = "John.Doe@Revature.net", Password = "StrondPassword123", Products = new List<ProductDTO>()};
            _repoMock.Setup(repo => repo.Update(It.IsAny<Seller>())).Throws(new Exception("Test exception"));

            // Act
            var result = _sellerService.Update(1, sellerToUpdateDTO);

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