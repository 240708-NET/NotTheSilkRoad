namespace Services;

using Models;
using Repository;
public class UserServices
{
    private DataContext _context;
    private UserRepository repo;
    // Constructor
    public User Create(UserRepository repo)
    {
        this.repo = repo;
    }
    // Get All
    public List<User> GetAll()
    {
        return repo.List();
    }
    // Get By ID
    public User GetById(int id)
    {
        return repo.GetById(id);
    }
    // Save/Create
    public User Save(User model)
    {
        return repo.Save(model);
    }
    // Delete by ID
    public User DeleteById(int id)
    {
        User target = repo.GetById(id);
        return repo.Delete(target);
    }
    // Update
    public User Update(int id, User model)
    {
        return repo.Update(id, model);
    }
}
