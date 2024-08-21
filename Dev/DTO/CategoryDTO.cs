using Models;

namespace DTO;

public class CategoryDTO
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public List<ProductDTO> Products { get; set; } = [];


    public CategoryDTO(Category entity, bool withProductList)
    {
        this.Id = entity.Id;
        this.Description = entity.Description;
        if(withProductList){
            entity.Products.ForEach(p => this.Products.Add(new ProductDTO(p, false)));
        }
    }

    public CategoryDTO()
    {
    }
}
