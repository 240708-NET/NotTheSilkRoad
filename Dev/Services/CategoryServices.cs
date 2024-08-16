namespace Services;

using Models;
using Repository;
public class CategoryServices
{
    private DataContext _context;
    private CategoryRepository repo;
    // Constructor
    public Category Create(CategoryRepository repo)
    {
        this.repo = repo;
    }
    // Get All
    public List<Category> GetAll()
    {
        return repo.List();
    }
    // Get By ID
    public Category GetById(int id)
    {
        return repo.GetById(id);
    }
    // Save/Create
    public Category Save(Category model)
    {
        return repo.Save(model);
    }
    // Delete by ID
    public Category DeleteByID(int id)
    {
        Category target = repo.GetById(id);
        return repo.Delete(model);
    }
    // Update
    public Category Update(int id, Category model)
    {
        return repo.Update(id, model);
    }
}
