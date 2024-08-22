using System.Runtime.CompilerServices;
using DTO;
using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class CategoryServices
{
    private readonly ILogger<Category> _logger;

    private ICategoryRepository _repo;

    private IProductRepository _repoProduct;

    public CategoryServices(ICategoryRepository repo,
                            IProductRepository repoProduct,
                             ILogger<Category> logger)
    {
        _repo = repo;
        _logger = logger;
        _repoProduct = repoProduct;
    }

    public List<CategoryDTO> GetAll()
    {
        try
        {
            return new List<CategoryDTO>(_repo.List().Select(c => new CategoryDTO(c, true)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public CategoryDTO? GetById(int id)
    {
        try
        {
            return new CategoryDTO(_repo.GetById(id), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public CategoryDTO? Save(CategoryDTO dto)
    {
        try
        {
            Category entity = (Category)RuntimeHelpers.GetUninitializedObject(typeof(Category));

            Category categoryCreated = _repo.Save(CopyDtoToEntity(dto, entity));

            return new CategoryDTO(categoryCreated, true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
        
    }

    public void Delete(CategoryDTO dto)
    {
        try
        {
            _repo.Delete(_repo.GetById(dto.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }

    public CategoryDTO? Update(int id, CategoryDTO dto)
    {
        try
        {
            Category category = _repo.GetById(id);
            return new CategoryDTO(_repo.Update(CopyDtoToEntity(dto, category)), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    private Category CopyDtoToEntity(CategoryDTO dto, Category entity){
        try
        {
            entity.Id = dto.Id;
            entity.Description = dto.Description;
            entity.Products = [];
            foreach(ProductDTO productDTO in dto.Products){
                Product product = _repoProduct.GetById(productDTO.Id);
                entity.Products.Add(product);
            }

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }

    }
}
