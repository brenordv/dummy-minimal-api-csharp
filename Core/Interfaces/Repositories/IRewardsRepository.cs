using Core.Models;

namespace Core.Interfaces.Repositories;

public interface IRewardsRepository
{
    IEnumerable<RewardItem> Get();
    RewardItem Get(Guid rewardId);
    IEnumerable<RewardItem> GetEligibleByPoints(int rewardPoints);
    void Add(RewardItem reward);
}