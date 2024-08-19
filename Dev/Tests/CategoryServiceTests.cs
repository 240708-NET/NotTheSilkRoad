using Moq;
using System.Collections.Generic;
using Xunit;
using Models;
using Services;
using Repository;
using Microsoft.Extensions.Logging;

namespace Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void GetAll_CorrectOutput()
        {
            // Arrange
            Mock<IRepository<Category>> repo = new Mock<IRepository<Category>>();
            Mock<ILogger<Category>> logger = new Mock<ILogger<Category>>();

            List<Category> expectedCategories = new List<Category>
            {
                new Category {Id = 1, Description = "Category 1", Products = new List<Product>()},
                new Category {Id = 2, Description = "Category 2", Products = new List<Product>()},
                new Category {Id = 3, Description = "Category 3", Products = new List<Product>()}
            };
            repo.Setup(x => x.List()).Returns(expectedCategories);
            CategoryServices categoryService = new CategoryServices(repo.Object, logger.Object);

            // Act
            var result = categoryService.GetAll();
            
            // Assert
            Assert.Equal(expectedCategories, result);
        }


    }
}
