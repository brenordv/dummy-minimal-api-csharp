using System.Diagnostics;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Core.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<IUserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public IEnumerable<User> Get()
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            return _userRepository.Get();
        }
        finally
        {
            sw.Stop();
            _logger.LogTrace("Call to '{MethodName}' completed. Elapsed time: {ElapsedTime}",
                "Get()", sw.Elapsed);
        }
    }

    public User Get(Guid userId)
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            if (userId == Guid.Empty)
                throw new DemoServiceException("Invalid user id.");

            var user = _userRepository.Get(userId);

            if (user == null)
                throw new DemoUserNotFoundException(userId);

            return user;
        }
        finally
        {
            sw.Stop();
            _logger.LogTrace("Call to '{MethodName}' completed. Elapsed time: {ElapsedTime}",
                "Get(Guid userId)", sw.Elapsed);
        }
    }

    public void Add(User user)
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            if (user == null)
                throw new DemoServiceException("Really? You want to add null to the database?");

            if (user.Id != Guid.Empty)
                throw new DemoServiceException("IDs are assigned automatically to new users.");

            user.Id = Guid.NewGuid();

            _userRepository.Add(user);
        }
        catch (DemoRepositoryException e)
        {
            throw new DemoServiceException("Failed to add user. Database didn't like this request.", e);
        }
        finally
        {
            sw.Stop();
            _logger.LogTrace("Call to '{MethodName}' completed. Elapsed time: {ElapsedTime}",
                "Add(User user)", sw.Elapsed);
        }
    }
}