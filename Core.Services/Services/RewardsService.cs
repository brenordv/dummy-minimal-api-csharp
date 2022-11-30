using System.Diagnostics;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Core.Services.Services;

public class RewardsService: IRewardsService
{
    private readonly IRewardsRepository _rewardsRepository;
    private readonly ILogger<IRewardsService> _logger;
    public RewardsService(IRewardsRepository rewardsRepository, ILogger<RewardsService> logger)
    {
        _rewardsRepository = rewardsRepository;
        _logger = logger;
    }

    public IEnumerable<RewardItem> Get()
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            return _rewardsRepository.Get();
        }
        finally
        {
            sw.Stop();
            _logger.LogTrace("Call to '{MethodName}' completed. Elapsed time: {ElapsedTime}",
                "Get()", sw.Elapsed);
        }
    }

    public RewardItem Get(Guid rewardId)
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            if (rewardId == Guid.Empty)
                throw new DemoServiceException("Invalid reward id.");

            var user = _rewardsRepository.Get(rewardId);

            if (user == null)
                throw new DemoUserNotFoundException(rewardId);

            return user;
        }
        finally
        {
            sw.Stop();
            _logger.LogTrace("Call to '{MethodName}' completed. Elapsed time: {ElapsedTime}",
                "Get(Guid rewardId)", sw.Elapsed);
        }
    }

    public IEnumerable<RewardItem> GetEligibleByPoints(int rewardPoints)
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            if (rewardPoints <= 0)
                throw new DemoNoContentException();

            var rewards = _rewardsRepository.GetEligibleByPoints(rewardPoints);

            if (rewards == null)
                throw new DemoNoContentException();

            return rewards;
        }
        finally
        {
            sw.Stop();
            _logger.LogTrace("Call to '{MethodName}' completed. Elapsed time: {ElapsedTime}",
                "Get(Guid rewardId)", sw.Elapsed);
        }
    }

    public RewardItem Add(RewardItem reward)
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            if (reward == null)
                throw new DemoServiceException("Really? You want to add null to the database?");

            if (reward.Id != Guid.Empty)
                throw new DemoServiceException("IDs are assigned automatically to new users.");

            reward.Id = Guid.NewGuid();

            _rewardsRepository.Add(reward);
            return reward;
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