using Models;

namespace Repository
{
    public class UserRepository : IRepository<User>
    {
        DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User Save(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Update(int Id, User user)
        {
            User userToUpdate = _context.Users.Find(Id);

            if (user != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                _context.SaveChanges();
                return userToUpdate;
            }
            return null;
        }

        public List<User> List()
        {
            return _context.Users.ToList();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetById(int Id)
        {
            return _context.Users.Find(Id);
        }
    }
}