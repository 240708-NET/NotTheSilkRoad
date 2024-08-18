using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class CategoryServices
{
    private readonly ILogger<Category> _logger;

    private IRepository<Category> _repo;

    // Constructor
    public CategoryServices(IRepository<Category> repo,
                             ILogger<Category> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    // Get All
    public List<Category> GetAll()
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
    public Category? GetById(int id)
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
    public Category? Save(Category category)
    {
        try
        {
            return _repo.Save(category);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
        
    }
    // Delete by ID
    public void Delete(Category category)
    {
        try
        {
            _repo.Delete(category);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
    // Update
    public Category? Update(int id, Category category)
    {
        try
        {
            return _repo.Update(id, category);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}
