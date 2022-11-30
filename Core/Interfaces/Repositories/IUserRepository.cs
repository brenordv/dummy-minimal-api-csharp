using Core.Models;

namespace Core.Interfaces.Repositories;

public interface IUserRepository
{
    IEnumerable<User> Get();
    User Get(Guid userId);
    void Add(User user);
}