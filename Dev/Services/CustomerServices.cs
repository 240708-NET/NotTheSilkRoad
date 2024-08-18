using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class CustomerServices
{
    private readonly ILogger<Customer> _logger;

    private IRepository<Customer> _repo;

    // Constructor
    public CustomerServices(IRepository<Customer> repo,
                            ILogger<Customer> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    // Get All
    public List<Customer> GetAll()
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
    public Customer? GetById(int id)
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
    public Customer Save(Customer customer)
    {
        try
        {
            return _repo.Save(customer);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    // Delete by ID
    public void Delete(Customer customer)
    {
        try
        {
            _repo.Delete(customer);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
    // Update
    public Customer Update(int id, Customer customer)
    {
        try
        {
            return _repo.Update(id, customer);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}
