using Core.Converters;
using Core.Enums;

namespace Core.Models;

public class User
{
    private DateTime _birthDate;
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public DateTime BirthDate
    {
        get => _birthDate.Date; 
        set => _birthDate = value;
    }
    public bool IsActive { get; set; }
    public int RewardPoints { get; set; }
    public int Age => DateTime.UtcNow.Year - BirthDate.Year;
    public UserTier Tier => UserConverters.RewardPointsToUserTier(RewardPoints);
}