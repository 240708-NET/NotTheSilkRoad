using Models;

namespace Repository
{
    public interface IUserRepository
    {
        User GetByEmail(string Email);
    }
}