using Models;

namespace DTO;

public class OrderDTO
{
    public int Id { get; set; }

    public CustomerDTO? Customer { get; set; }

    public List<ItemDTO> Items { get; set; } = [];

    public DateOnly Date { get; set; }

    public bool Active { get; set; }

    public string? ShippingAddress { get; set; }


    public OrderDTO(Order entity)
    {
        this.Id = entity.Id;
        this.Customer = entity.Customer != null ? new CustomerDTO(entity.Customer, false) : null;
        entity.Items.ForEach(i => this.Items.Add(new ItemDTO(i)));
        this.Date = entity.Date;
        this.Active = entity.Active;
        this.ShippingAddress = entity.ShippingAddress;
    }
    
    public OrderDTO()
    {
    }

}
