using Models;

namespace DTO;

public class ItemDTO
{
    public int Id { get; set; }

    public ProductDTO? Product { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public ItemDTO(Item entity)
    {
        this.Id = entity.Id;
        this.Product = entity.Product != null ? new ProductDTO(entity.Product, true) : null;
        this.Quantity = entity.Quantity;
        this.Price = entity.Price;
    }

    public ItemDTO()
    {
    }

}
