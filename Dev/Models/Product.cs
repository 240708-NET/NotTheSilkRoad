using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    [Precision(18, 2)]
    public required decimal Price { get; set; }

    public Seller? Seller { get; set; }

    public List<Category> Categories { get; set; } = [];
    public int Quantity {get;set;}

    public string? ImageUrl {get;set;}
}
