using System.Runtime.CompilerServices;
using DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class CustomerServices
{
    private readonly ILogger<Customer> _logger;

    private ICustomerRepository _repo;

    private IOrderRepository _repoOrder;

    public CustomerServices(ICustomerRepository repo,
                            IOrderRepository repoOrder,
                            ILogger<Customer> logger)
    {
        _repo = repo;
        _logger = logger;
        _repoOrder = repoOrder;
    }

    public List<CustomerDTO> GetAll()
    {
        try
        {
            return new List<CustomerDTO>(_repo.List().Select(c => new CustomerDTO(c, true)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public CustomerDTO? GetById(int id)
    {
        try
        {
            return new CustomerDTO(_repo.GetById(id), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public CustomerDTO? Save(CustomerDTO dto)
    {
        try
        {
            Customer entity = (Customer)RuntimeHelpers.GetUninitializedObject(typeof(Customer));

            Customer customerCreated = _repo.Save(CopyDtoToEntity(dto, entity));

            return new CustomerDTO(customerCreated, true);
        }
        catch (SqlException ex) when (ex.Number == 2601)
        {
            _logger.LogError(ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public void Delete(CustomerDTO dto)
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

    public CustomerDTO Update(int id, CustomerDTO dto)
    {
        try
        {
            Customer customer = _repo.GetById(id);
            return new CustomerDTO(_repo.Update(CopyDtoToEntity(dto, customer)), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    private Customer CopyDtoToEntity(CustomerDTO dto, Customer entity){
        try
        {
            
            entity.Id = dto.Id;
            entity.Address = dto.Address;
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            if(dto.Password != null && !dto.Password.Equals("")){
                entity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password) ;
            }
            entity.Orders = [];
            foreach(OrderDTO orderDTO in dto.Orders){
                Order order = _repoOrder.GetById(orderDTO.Id);
                entity.Orders.Add(order);
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
