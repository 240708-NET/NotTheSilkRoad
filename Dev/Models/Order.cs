using System.ComponentModel.DataAnnotations;

namespace Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    public required Customer Customer { get; set; }

    public List<Item> Items { get; set; } = [];

    public DateOnly Date { get; set; }

    public bool Active { get; set; }

    public required string ShippingAddress { get; set; }

    public Order()
    {
    }

}
