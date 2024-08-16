namespace Services;

using Models;
using Repository;
public class OrderServices
{
    private DataContext _context;
    private OrderRepository repo;
    // Constructor
    public Order Create(OrderRepository repo)
    {
        this.repo = repo;
    }
    // Get All
    public List<Order> GetAll()
    {
        return repo.List();
    }
    // Get By ID
    public Order GetById(int id)
    {
        return repo.GetById(id);
    }
    // Save/Create
    public Order Save(Order model)
    {
        return repo.Save(model);
    }
    // Delete by ID
    public Order DeleteById(int id)
    {
        Order target = repo.GetById(id);
        return repo.Delete(target);
    }
    // Update
    public Order Update(int id, Order model)
    {
        return repo.Update(id, model);
    }
}