using Core.DataGeneration;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;

namespace Core.Repositories.Repositories;

public class RewardsRepository: IRewardsRepository
{
    private static readonly List<RewardItem> RewardsDb = ModelGenerator.GenerateRewardItems(500).ToList();
    public IEnumerable<RewardItem> Get()
    {
        return RewardsDb;
    }

    public RewardItem Get(Guid rewardId)
    {
        return RewardsDb.FirstOrDefault(reward => reward.Id == rewardId);
    }

    public IEnumerable<RewardItem> GetEligibleByPoints(int rewardPoints)
    {
        return RewardsDb.Where(reward => reward.Cost <= rewardPoints && reward.IsAvailable);
    }

    public void Add(RewardItem reward)
    {
        var idExists = Get(reward.Id) != null;
        if (idExists)
            throw new DemoRepositoryException($"Reward ID '{reward.Id}' already exist.");
        
        RewardsDb.Add(reward);
    }
}