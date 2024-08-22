using System.Runtime.CompilerServices;
using DTO;
using Microsoft.Extensions.Logging;
using Models;
using Repository;

namespace Services;
public class OrderServices
{
    private readonly ILogger<Order> _logger;

    private IOrderRepository _repo;

    private ICustomerRepository _repoCustomer;

    private IItemRepository _repoItem;


    public OrderServices(IOrderRepository repo,
                        ICustomerRepository repoCustomer,
                        IItemRepository repoItem,
                         ILogger<Order> logger)
    {
        _repo = repo;
        _logger = logger;
        _repoCustomer = repoCustomer;
        _repoItem = repoItem;
    }

    public List<OrderDTO> GetAll()
    {
        try
        {
            return new List<OrderDTO>(_repo.List().Select(o => new OrderDTO(o)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public OrderDTO? GetById(int id)
    {
        try
        {
            return new OrderDTO(_repo.GetById(id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public List<OrderDTO> GetByCustomerId(int id)
    {
        try
        {
            return new List<OrderDTO>(_repo.GetByCustomerId(id).Select(o => new OrderDTO(o)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public OrderDTO? Save(OrderDTO dto)
    {
        try
        {   
            Order entity = (Order)RuntimeHelpers.GetUninitializedObject(typeof(Order));

            Order orderCreated = _repo.Save(CopyDtoToEntity(dto, entity));

            return new OrderDTO(orderCreated);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public void Delete(OrderDTO dto)
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

    public OrderDTO? Update(int id, OrderDTO dto)
    {
        try
        {
            Order order = _repo.GetById(id);
            return new OrderDTO(_repo.Update(CopyDtoToEntity(dto, order)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    private Order CopyDtoToEntity(OrderDTO dto, Order entity){
        try
        {
            entity.Id = dto.Id;
            entity.Customer = _repoCustomer.GetById(dto.Customer.Id);
            entity.Items = [];
            foreach(ItemDTO itemDTO in dto.Items){
                Item item = _repoItem.GetById(itemDTO.Id);
                entity.Items.Add(item);
            }
            entity.Date = dto.Date;
            entity.Active = dto.Active;
            entity.ShippingAddress = dto.ShippingAddress;
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }

    }
}