using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class ItemServices
{
    private readonly ILogger<Item> _logger;

    private IRepository<Item> _repo;

    // Constructor
    public ItemServices(IRepository<Item> repo,
                        ILogger<Item> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    // Get All
    public List<Item> GetAll()
    {
        try
        {
            return _repo.List();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }
    // Get By ID
    public Item? GetById(int id)
    {
        try
        {
            return _repo.GetById(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    // Save/Create
    public Item? Save(Item item)
    {
        try
        {
            return _repo.Save(item);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    // Delete by ID
    public void Delete(Item item)
    {
        try
        {
            _repo.Delete(item);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
    // Update
    public Item? Update(int id, Item item)
    {
        try
        {
            return _repo.Update(id, item);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}