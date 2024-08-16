namespace Services;

using Models;
using Repository;
public class ProductServices
{
    private DataContext _context;
    private ProductRepository repo;
    // Constructor
    public Product Create(ProductRepository repo)
    {
        this.repo = repo;
    }
    // Get All
    public List<Product> GetAll()
    {
        return repo.List();
    }
    // Get By ID
    public Product GetById(int id)
    {
        return repo.GetById(id);
    }
    // Save/Create
    public Product Save(Product model)
    {
        return repo.Save(model);
    }
    // Delete by ID
    public Product DeleteById(int id)
    {
        Product target = repo.GetById(id);
        return repo.Delete(target);
    }
    // Update
    public Product Update(int id, Product model)
    {
        return repo.Update(id, model);
    }
}