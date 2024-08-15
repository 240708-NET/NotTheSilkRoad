namespace Models;

public class Customer : User
{
    public required string Address { get; set; }

    public List<Order>? Orders { get; set; } = [];
}
