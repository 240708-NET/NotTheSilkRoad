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
    public class CategoryServiceTests
    {
        private readonly Mock<IRepository<Category>> _repoMock;
        private readonly Mock<ILogger<Category>> _loggerMock;
        private readonly CategoryServices _categoryService;

        public CategoryServiceTests()
        {
            _repoMock = new Mock<IRepository<Category>>();
            _loggerMock = new Mock<ILogger<Category>>();
            _categoryService = new CategoryServices(_repoMock.Object, _loggerMock.Object);
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

            // Act
            var result = _categoryService.GetAll();

            // Assert
            Assert.Equal(expectedCategories, result);
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

            // Act
            var result = _categoryService.GetById(1);

            // Assert
            Assert.Equal(expectedCategory, result);
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
            _repoMock.Setup(x => x.Save(newCategory)).Returns(newCategory);

            // Act
            var result = _categoryService.Save(newCategory);

            // Assert
            Assert.Equal(newCategory, result);
        }

        [Fact]
        public void Save_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            _repoMock.Setup(repo => repo.Save(It.IsAny<Category>())).Throws(new Exception("Test exception"));

            // Act
            var result = _categoryService.Save(new Category { Id = 1, Description = "Test", Products = new List<Product>() });

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

            // Act
            _categoryService.Delete(categoryToDelete);

            // Assert
            _repoMock.Verify(x => x.Delete(categoryToDelete), Times.Once);
        }

        [Fact]
        public void Delete_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            Category categoryToDelete = new Category { Id = 1, Description = "Category to delete", Products = new List<Product>() };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Category>())).Throws(new Exception("Test exception"));

            // Act
            _categoryService.Delete(categoryToDelete);

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
            _repoMock.Setup(x => x.Update(1, updatedCategory)).Returns(updatedCategory);

            // Act
            var result = _categoryService.Update(1, updatedCategory);

            // Assert
            Assert.Equal(updatedCategory, result);
        }

        [Fact]
        public void Update_WhenExceptionThrown_ShouldLogErrorAndReturnNull()
        {
            // Arrange
            Category categoryToUpdate = new Category { Id = 1, Description = "Updated Category", Products = new List<Product>() };
            _repoMock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Category>())).Throws(new Exception("Test exception"));

            // Act
            var result = _categoryService.Update(1, categoryToUpdate);

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
