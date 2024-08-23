using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class UserServices
{
    private readonly ILogger<Seller> _logger;
    private IUserRepository _repo;

    public UserServices(IUserRepository repo,
                            ILogger<Seller> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public User? GetByEmail(string Email){
        try
        {
            return _repo.GetByEmail(Email);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

}
