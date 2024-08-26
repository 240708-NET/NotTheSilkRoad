using Models;

namespace DTO;

public class ProductDTO
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public SellerDTO? Seller { get; set; }

    public List<CategoryDTO> Categories { get; set; } = [];

    public int Quantity {get;set;}

    public string? ImageUrl {get;set;}

    public ProductDTO(Product entity, bool withCategoryList){
        this.Id = entity.Id;
        this.Title = entity.Title;
        this.Description = entity.Description;
        this.Price = entity.Price;
        this.Seller = entity.Seller != null ? new SellerDTO(entity.Seller, false) : null;
        if(withCategoryList){
            entity.Categories.ForEach(p => this.Categories.Add(new CategoryDTO(p, false)));
        }
        this.Quantity = entity.Quantity;
        this.ImageUrl = entity.ImageUrl;
    }   

    public ProductDTO(){
    }
}
