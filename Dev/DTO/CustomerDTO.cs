using Models;

namespace DTO;

public class CustomerDTO : UserDTO
{
    public string? Address { get; set; }

    public List<OrderDTO>? Orders { get; set; } = [];
    
    public CustomerDTO(Customer entity, bool withOrdersList)
    {
        this.Id = entity.Id;
        this.Address = entity.Address;
        if(withOrdersList){
            entity.Orders.ForEach(o => this.Orders.Add(new OrderDTO(o)));
        }
        this.Name = entity.Name;
        this.Email = entity.Email;
    }

    public CustomerDTO()
    {
    }
    
}
