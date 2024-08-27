using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public Order Save(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
            return order;
        }

        public List<Order> List()
        {
            return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToList();
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

        public List<Order> GetByCustomerId(int Id)
        {
            return _context.Orders.Where(o => o.Customer.Id == Id)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToList();
        }
    }
}