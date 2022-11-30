using Core.DataGeneration;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;

namespace Core.Repositories.Repositories;

public class UserRepository: IUserRepository
{
    private static readonly List<User> UsersDb = ModelGenerator.GenerateUsers(500).ToList();
    
    public IEnumerable<User> Get()
    {
        return UsersDb;
    }

    public User Get(Guid userId)
    {
        return UsersDb.FirstOrDefault(user => user.Id == userId);
    }

    public void Add(User user)
    {
        var idExists = Get(user.Id) != null;
        if (idExists)
            throw new DemoRepositoryException($"User ID '{user.Id}' already exist.");
        
        UsersDb.Add(user);
    }
}