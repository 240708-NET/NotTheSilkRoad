using System.ComponentModel.DataAnnotations;

namespace Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required decimal Price { get; set; }

    public Seller? Seller { get; set; }

    public List<Category> Categories { get; set; } = [];
}
