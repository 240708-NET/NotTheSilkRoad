using Models;

namespace Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetByCustomerId(int Id);
    }
}