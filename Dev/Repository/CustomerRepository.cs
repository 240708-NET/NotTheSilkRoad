using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        DataContext _context;
        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public Customer Save(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return customer;
        }

        public List<Customer> List()
        {
            return _context.Customers
            .Include(c => c.Orders)
            .ToList();
        }

        public void Delete(Customer customer)
        {
            _context.Users.Remove(customer);
            _context.SaveChanges();
        }

        public Customer GetById(int Id)
        {
            return _context.Customers.Find(Id);
        }
    }
}