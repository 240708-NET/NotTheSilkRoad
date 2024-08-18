using Models;

namespace Repository
{
    public class ProductRepository : IRepository<Product>
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

        public Product Update(int Id, Product product)
        {
            Product productToUpdate = _context.Products.Find(Id);

            if (product != null)
            {
                productToUpdate.Title = product.Title;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Seller = _context.Sellers.Find(product.Seller.Id);
                _context.SaveChanges();
                return productToUpdate;
            }
            return null;
        }

        public List<Product> List()
        {
            return _context.Products.ToList();
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
    }
}