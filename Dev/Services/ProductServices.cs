using System.Runtime.CompilerServices;
using DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class ProductServices
{
    private readonly ILogger<Product> _logger;
    private IProductRepository _repo;
    private ISellerRepository _repoSeller;
    private ICategoryRepository _repoCategory;

    public ProductServices(IProductRepository repo,
                            ISellerRepository repoSeller,
                            ICategoryRepository repoCategory,
                            ILogger<Product> logger)
    {
        _repo = repo;
        _logger = logger;
        _repoSeller = repoSeller;
        _repoCategory = repoCategory;
    }

    public List<ProductDTO> GetAll()
    {
        try
        {
            return new List<ProductDTO>(_repo.List().Select(p => new ProductDTO(p, true)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public ProductDTO GetById(int id)
    {
        try
        {
            return new ProductDTO(_repo.GetById(id), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

        public List<ProductDTO> GetBySellerId(int id)
    {
        try
        {
            return new List<ProductDTO>(_repo.GetBySellerId(id).Select(p => new ProductDTO(p, true)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public ProductDTO Save(ProductDTO dto)
    {
        try
        {
            Product entity = (Product)RuntimeHelpers.GetUninitializedObject(typeof(Product));

            Product productCreated = _repo.Save(CopyDtoToEntity(dto, entity));

            return new ProductDTO(productCreated, true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public void Delete(ProductDTO dto)
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

    public ProductDTO Update(int id, ProductDTO dto)
    {
        try
        {
            Product product = _repo.GetById(id);
            return new ProductDTO(_repo.Update(CopyDtoToEntity(dto, product)), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    private Product CopyDtoToEntity(ProductDTO dto, Product entity){
        try
        {
            entity.Id = dto.Id;
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Seller = dto.Seller != null ? _repoSeller.GetById(dto.Seller.Id) : null;
            entity.Quantity = dto.Quantity;
            entity.ImageUrl = dto.ImageUrl;
            entity.Categories = [];
            foreach(CategoryDTO categoryDTO in dto.Categories){
                Category category = _repoCategory.GetById(categoryDTO.Id);
                entity.Categories.Add(category);
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