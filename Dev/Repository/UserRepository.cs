using Models;

namespace Repository
{
    /**
        Do not implementy any other method here. Remember User is just a superclass
        we never save/update/delete User.
    **/
    public class UserRepository : IUserRepository
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