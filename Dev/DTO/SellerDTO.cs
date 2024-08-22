using Models;

namespace DTO;

public class SellerDTO : UserDTO
{
    public List<ProductDTO>? Products { get; set; } = [];

    public SellerDTO(Seller entity, bool withProductList)
    {
        this.Id = entity.Id;
        if(withProductList){
            entity.Products.ForEach(p => this.Products.Add(new ProductDTO(p, true)));
        }
        this.Name = entity.Name;
        this.Email = entity.Email;
    }

    public SellerDTO()
    {
    }
}
