using Core.Models;

namespace Core.Interfaces.Services;

public interface IUserService
{
    IEnumerable<User> Get();
    User Get(Guid userId);
    void Add(User user);
}