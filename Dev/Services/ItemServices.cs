using System.Runtime.CompilerServices;
using DTO;
using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class ItemServices
{
    private readonly ILogger<Item> _logger;

    private IItemRepository _repo;

    private IProductRepository _repoProduct;

    public ItemServices(IItemRepository repo,
                        IProductRepository repoProduct,
                        ILogger<Item> logger)
    {
        _repo = repo;
        _logger = logger;
        _repoProduct = repoProduct;
    }

    public List<ItemDTO> GetAll()
    {
        try
        {
            return new List<ItemDTO>(_repo.List().Select(i => new ItemDTO(i)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public ItemDTO? GetById(int id)
    {
        try
        {
            return new ItemDTO(_repo.GetById(id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public ItemDTO? Save(ItemDTO dto)
    {
        try
        {
            Item entity = (Item)RuntimeHelpers.GetUninitializedObject(typeof(Item));

            Item itemCreated = _repo.Save(CopyDtoToEntity(dto, entity));

            return new ItemDTO(itemCreated);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public void Delete(ItemDTO dto)
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

    public ItemDTO? Update(int id, ItemDTO dto)
    {
        try
        {
            Item item = _repo.GetById(id);
            return new ItemDTO(_repo.Update(CopyDtoToEntity(dto, item)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    private Item CopyDtoToEntity(ItemDTO dto, Item entity){
        try
        {
            entity.Id = dto.Id;
            entity.Product = _repoProduct.GetById(dto.Product.Id);
            entity.Quantity = dto.Quantity;
            entity.Price = dto.Price;

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }

    }
}