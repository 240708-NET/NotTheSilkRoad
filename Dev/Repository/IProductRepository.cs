using Models;

namespace Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetBySellerId(int Id);
    }
}