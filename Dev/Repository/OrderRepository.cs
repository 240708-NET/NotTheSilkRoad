using Models;

namespace Repository
{
    public class OrderRepository : IRepository<Order>
    {
        DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        Order Save(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        Order Update(int Id, Order order)
        {
            Order orderToUpdate = _context.Order.Find(Id);

            if (order != null)
            {
                orderToUpdate.CustomerId = order.CustomerId;
                orderToUpdate.Date = order.Date;
                orderToUpdate.Active = order.Active;
                orderToUpdate.ShippingAddress = order.ShippingAddress;
                _context.SaveChanges();
                return _context.Order.Find(Id);
            }
            return null;
        }

        List<Order> List()
        {
            return _context.Orders.ToList();
        }

        void Delete(Order order)
        {
            _context.Orders.Remove(item);
            _context.SaveChanges();
        }

        Order GetById(int Id)
        {
            return _context.Orders.Find(Id);
        }
    }
}