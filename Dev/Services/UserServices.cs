using System.Runtime.CompilerServices;
using DTO;
using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class UserServices
{
    private readonly ILogger<User> _logger;

    private IRepository<User> _repo;

    private IRepository<Order> _repoOrder;

    public UserServices(IRepository<User> repo,
                            IRepository<Order> repoOrder,
                            ILogger<User> logger)
    {
        _repo = repo;
        _logger = logger;
        _repoOrder = repoOrder;
    }

    public List<UserDTO> GetAll()
    {
        try
        {
            return new List<UserDTO>(_repo.List().Select(c => new UserDTO(c)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public UserDTO? GetById(int id)
    {
        try
        {
            User target= _repo.GetById(id);
            return new UserDTO(target);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public UserDTO? Save(UserDTO dto)
    {
        try
        {
            User entity = (User)RuntimeHelpers.GetUninitializedObject(typeof(User));

            User customerCreated = _repo.Save(CopyDtoToEntity(dto,entity));

            return new UserDTO(entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public void Delete(UserDTO dto)
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

    public UserDTO Update(int id, UserDTO dto)
    {
        try
        {
            User customer = _repo.GetById(id);
            return new UserDTO(customer);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    private User CopyDtoToEntity(UserDTO dto, User entity){
        try
        {
            entity.Id = dto.Id;
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Password = dto.Password;
         
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }

    }
}
