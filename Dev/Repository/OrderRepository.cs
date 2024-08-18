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

        public Order Save(Order order)
        {
            order.Customer = _context.Customers.Find(order.Customer.Id);
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order Update(int Id, Order order)
        {
            Order orderToUpdate = _context.Orders.Find(Id);

            if (order != null)
            {
                orderToUpdate.Customer = _context.Customers.Find(order.Customer.Id);
                orderToUpdate.Date = order.Date;
                orderToUpdate.Active = order.Active;
                orderToUpdate.ShippingAddress = order.ShippingAddress;
                _context.SaveChanges();
                return orderToUpdate;
            }
            return null;
        }

        public List<Order> List()
        {
            return _context.Orders.ToList();
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public Order GetById(int Id)
        {
            return _context.Orders.Find(Id);
        }
    }
}