using Models;

namespace Repository
{
    public class UserRepository
    {
        DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetByEmail(string Email)
        {

            return _context.Users.Where(u => u.Email == Email).FirstOrDefault<User>();
            
        }
    }
}