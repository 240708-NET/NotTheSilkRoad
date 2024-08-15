using System.ComponentModel.DataAnnotations;

namespace Models;

public class Item
{
    [Key]
    public int Id { get; set; }

    public required Product Product { get; set; }

    public required decimal Quantity { get; set; }

    public required decimal Price { get; set; }

}
