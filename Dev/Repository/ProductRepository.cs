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

        Product Save(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product;
        }

        Product Update(int Id, Product product)
        {
            Product productToUpdate = _context.Product.Find(Id);

            if (order != null)
            {
                productToUpdate.Title = product.Title;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.SellerId = product.SellerId;
                _context.SaveChanges();
                return _context.Product.Find(Id);
            }
            return null;
        }

        List<Product> List()
        {
            return _context.Products.ToList();
        }

        void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        Product GetById(int Id)
        {
            return _context.Products.Find(Id);
        }
    }
}