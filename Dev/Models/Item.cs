using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Models;

public class Item
{
    [Key]
    public int Id { get; set; }

    public required Product Product { get; set; }

    [Precision(18, 2)]
    public required decimal Quantity { get; set; }

    [Precision(18, 2)]
    public required decimal Price { get; set; }

}
