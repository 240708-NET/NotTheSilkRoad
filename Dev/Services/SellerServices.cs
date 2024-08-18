using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class SellerServices
{
    private readonly ILogger<Seller> _logger;
    private IRepository<Seller> _repo;

    // Constructor
    public SellerServices(IRepository<Seller> repo,
                            ILogger<Seller> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    // Get All
    public List<Seller> GetAll()
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
    public Seller? GetById(int id)
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
    public Seller? Save(Seller seller)
    {
        try
        {
            return _repo.Save(seller);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    // Delete by ID
    public void Delete(Seller seller)
    {
        try
        {
            _repo.Delete(seller);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
    // Update
    public Seller? Update(int id, Seller seller)
    {
        try
        {
            return _repo.Update(id, seller);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}
