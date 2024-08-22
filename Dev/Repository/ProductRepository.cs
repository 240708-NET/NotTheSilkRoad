using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public Product Save(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return product;
        }

        public List<Product> List()
        {
            return _context.Products
            .Include(p => p.Categories)
            .Include(p => p.Seller)
            .ToList();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product GetById(int Id)
        {
            return _context.Products.Find(Id);
        }

        public List<Product> GetBySellerId(int Id)
        {
            return _context.Products.Where(p => p.Seller.Id == Id).Include(p => p.Categories).ToList();
        }
    }
}