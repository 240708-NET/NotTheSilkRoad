using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class OrderServices
{
    private readonly ILogger<Order> _logger;

    private IRepository<Order> _repo;

    // Constructor
    public OrderServices(IRepository<Order> repo,
                         ILogger<Order> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    // Get All
    public List<Order> GetAll()
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
    public Order GetById(int id)
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
    public Order Save(Order order)
    {
        try
        {
            return _repo.Save(order);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    // Delete by ID
    public void Delete(Order order)
    {
        try
        {
            _repo.Delete(order);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
    // Update
    public Order Update(int id, Order order)
    {
        try
        {
            return _repo.Update(id, order);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}