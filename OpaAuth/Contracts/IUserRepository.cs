using OpaAuth.Models;

namespace OpaAuth.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        void Add(User user);
    }
}
