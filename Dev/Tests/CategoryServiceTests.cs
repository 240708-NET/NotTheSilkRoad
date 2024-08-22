using Moq;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Services;
using Repository;
using Models;
using DTO;

namespace Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _repoMock;
        private readonly Mock<IProductRepository> _repoProductMock;
        private readonly Mock<ILogger<Category>> _loggerMock;
        private readonly CategoryServices _categoryService;

        public CategoryServiceTests()
        {
            _repoMock = new Mock<ICategoryRepository>();
            _repoProductMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<Category>>();
            _categoryService = new CategoryServices(_repoMock.Object, _repoProductMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            List<Category> expectedCategories = new List<Category>
            {
                new Category {Id = 1, Description = "Category 1", Products = new List<Product>()},
                new Category {Id = 2, Description = "Category 2", Products = new List<Product>()},
                new Category {Id = 3, Description = "Category 3", Products = new List<Product>()}
            };
            _repoMock.Setup(x => x.List()).Returns(expectedCategories);
            List<CategoryDTO> expectedDTOs = expectedCategories.Select(i => new CategoryDTO(i, true)).ToList();

            // Act
            var result = _categoryService.GetAll();

            // Assert
            result.Should().BeEquivalentTo(expectedDTOs);
        }

        [Fact]
        public void GetAll_WhenExceptionThrown_ShouldLogErrorAndReturnEmptyList()
        {
            // Arrange
            _repoMock.Setup(repo => repo.List()).Throws(new Exception("Test exception"));

            // Act
            var result = _categoryService.GetAll();

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
            Category expectedCategory = new Category { Id = 1, Description = "Category 1", Products = new List<Product>() };
            _repoMock.Setup(x => x.GetById(1)).Returns(expectedCategory);
            CategoryDTO expectedDTO = new CategoryDTO(expectedCategory, true);

            // Act
            var result = _categoryService.GetById(1);

            // Assert
            result.Should().BeEquivalentTo(expectedDTO);
        }

        [Fact]
        public void GetById_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Test exception"));

            // Act
            var result = _categoryService.GetById(1);

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
            Category newCategory = new Category { Id = 1, Description = "New Category", Products = new List<Product>() };
            _repoMock.Setup(x => x.Save(It.IsAny<Category>())).Returns(newCategory);

            CategoryDTO newCategoryDTO = new CategoryDTO(newCategory, true);

            // Act
            var result = _categoryService.Save(newCategoryDTO);

            // Assert
            result.Should().BeEquivalentTo(newCategoryDTO);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Category>())).Throws(new Exception("Test exception"));

            // Act
            var result = _categoryService.Save(new CategoryDTO { Id = 1, Description = "Test", Products = new List<ProductDTO>() });

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
            Category categoryToDelete = new Category { Id = 1, Description = "Category to delete", Products = new List<Product>() };
            CategoryDTO categoryToDeleteDTO = new CategoryDTO(categoryToDelete, true);

            _repoMock.Setup(repo => repo.GetById(categoryToDeleteDTO.Id)).Returns(categoryToDelete);

            // Act
            _categoryService.Delete(categoryToDeleteDTO);

            // Assert
            _repoMock.Verify(x => x.Delete(categoryToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            CategoryDTO categoryToDeleteDTO = new CategoryDTO { Id = 1, Description = "Category to delete", Products = new List<ProductDTO>() };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Category>())).Throws(new Exception("Test exception"));

            // Act
            _categoryService.Delete(categoryToDeleteDTO);

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
            Category updatedCategory = new Category { Id = 1, Description = "Updated Category", Products = new List<Product>() };
            CategoryDTO updatedCategoryDTO = new CategoryDTO(updatedCategory, true);
            
            _repoMock.Setup(x => x.Update(It.IsAny<Category>())).Returns(updatedCategory);

            // Act
            var result = _categoryService.Update(1, updatedCategoryDTO);

            // Assert
            result.Should().BeEquivalentTo(updatedCategoryDTO);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            CategoryDTO categoryToUpdateDTO = new CategoryDTO { Id = 1, Description = "Updated Category", Products = new List<ProductDTO>() };
            _repoMock.Setup(repo => repo.Update(It.IsAny<Category>())).Throws(new Exception("Test exception"));

            // Act
            var result = _categoryService.Update(1, categoryToUpdateDTO);

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
