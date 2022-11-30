using Core.Enums;

namespace Core.Converters;

public static class UserConverters
{
    public static UserTier RewardPointsToUserTier(int rewardPoints)
    {
        return rewardPoints switch
        {
            < 10000 => UserTier.Normal,
            < 45000 => UserTier.Vip,
            < 75000 => UserTier.Premium,
            _ => UserTier.Platinum
        };
    }
}