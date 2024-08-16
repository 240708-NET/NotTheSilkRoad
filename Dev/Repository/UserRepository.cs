using Models;

namespace Repository
{
    public class UserRepository : IRepository<User>
    {
        DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        User Save(Item user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        };

        User Update(int Id, User user)
        {
            User userToUpdate = _context.User.Find(Id);

            if (user != null)
            {
                userToUpdate.name = user.name;
                userToUpdate.email = user.email;
                userToUpdate.password = user.password;
                userToUpdate.user_type = user.user_type;
                _context.SaveChanges();
                return _context.User.Find(Id);
            }
            return null;
        };

        List<User> List()
        {
            return _context.Users.ToList();
        };

        void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        };

        User GetById(int Id)
        {
            return _context.Users.Find(Id);
        };
    }
}