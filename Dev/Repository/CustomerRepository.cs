using Models;

namespace Repository
{
    public class CustomerRepository : IRepository<Customer>
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

        public Customer Update(int Id, Customer customer)
        {
            Customer customerToUpdate = _context.Customers.Find(Id);

            if (customer != null)
            {
                customerToUpdate.Name = customer.Name;
                customerToUpdate.Email = customer.Email;
                customerToUpdate.Password = customer.Password;
                customerToUpdate.Address = customer.Address;
                _context.SaveChanges();
                return customerToUpdate;
            }
            return null;
        }

        public List<Customer> List()
        {
            return _context.Customers.ToList();
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