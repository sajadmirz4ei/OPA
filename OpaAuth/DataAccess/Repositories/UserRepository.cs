using OpaAuth.Contracts;
using OpaAuth.Models;
using System.Runtime.Caching;

namespace OpaAuth.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MemoryCache _cache;
        private readonly string _cacheKey = "userListKey";
        private readonly CacheItemPolicy _policy;
        public UserRepository()
        {
            _cache = MemoryCache.Default;
            _policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) };
        }
        public void Add(User user)
        {
            var users = _cache.Get(_cacheKey) as List<User>;

            if (users == null)
            {
                users = new List<User>();
            }

            users.Add(user);
            _cache.Set(_cacheKey, users, _policy);
        }

        public IEnumerable<User> GetAll()
        {
            return _cache.Get(_cacheKey) as List<User> ?? new List<User>();
        }
    }
}
