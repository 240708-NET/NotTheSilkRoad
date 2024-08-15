using System.ComponentModel.DataAnnotations;

namespace Models;

public class Item
{
    [Key]
    public int Id { get; set; }

    public required Product Product { get; set; }

    public required float Quantity { get; set; }

    public required float Price { get; set; }

}
