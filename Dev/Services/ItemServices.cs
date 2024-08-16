namespace Services;

using Models;
using Repository;
public class ItemServices
{
    private DataContext _context;
    private ItemRepository repo;
    // Constructor
    public Item Create(ItemRepository repo)
    {
        this.repo = repo;
    }
    // Get All
    public List<Item> GetAll()
    {
        return repo.List();
    }
    // Get By ID
    public Item GetById(int id)
    {
        return repo.GetById(id);
    }
    // Save/Create
    public Item Save(Item model)
    {
        return repo.Save(model);
    }
    // Delete by ID
    public Item DeleteById(int id)
    {
        Item target = repo.GetById(id);
        return repo.Delete(target);
    }
    // Update
    public Item Update(int id, Item model)
    {
        return repo.Update(id, model);
    }
}