namespace Models;

public class Seller : User
{
    public List<Product> Products { get; set; } = [];
}
