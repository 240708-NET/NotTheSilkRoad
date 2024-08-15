using System.ComponentModel.DataAnnotations;

namespace Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    public required string Description { get; set; }

    public List<Product> Products { get; set; } = [];
}
