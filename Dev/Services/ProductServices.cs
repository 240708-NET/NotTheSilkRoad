using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class ProductServices
{
    private readonly ILogger<Product> _logger;
    private IRepository<Product> _repo;
    // Constructor
    public ProductServices(IRepository<Product> repo,
                            ILogger<Product> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    // Get All
    public List<Product> GetAll()
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
    public Product GetById(int id)
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
    public Product Save(Product product)
    {
        try
        {
            return _repo.Save(product);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    // Delete by ID
    public void Delete(Product product)
    {
        try
        {
            _repo.Delete(product);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
    // Update
    public Product Update(int id, Product product)
    {
        try
        {
            return _repo.Update(id, product);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}