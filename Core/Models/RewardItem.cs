using Core.Converters;
using Core.Enums;

namespace Core.Models;

public record RewardItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime IncludedAt { get; set; }
    public DateTime MadeAvailableAt { get; set; }
    public UserTier MinimumUserTier => UserConverters.RewardPointsToUserTier(Cost);
};