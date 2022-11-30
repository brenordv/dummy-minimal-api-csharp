using Core.Models;

namespace Core.Interfaces.Services;

public interface IRewardsService
{
    IEnumerable<RewardItem> Get();
    RewardItem Get(Guid rewardId);
    IEnumerable<RewardItem> GetEligibleByPoints(int rewardPoints);
    RewardItem Add(RewardItem reward);
}