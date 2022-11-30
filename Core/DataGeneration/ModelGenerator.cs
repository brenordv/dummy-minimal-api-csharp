using Bogus;
using Core.Models;

namespace Core.DataGeneration;

public static class ModelGenerator
{
    public static IEnumerable<User> GenerateUsers(int qty)
    {
        var generator = new Faker<User>()
            .RuleFor(user => user.Id, (faker, user) => faker.Random.Guid())
            .RuleFor(user => user.FirstName, (faker, user) => faker.Name.FirstName())
            .RuleFor(user => user.LastName, (faker, user) => faker.Name.LastName())
            .RuleFor(user => user.Email, (faker, user) => faker.Person.Email)
            .RuleFor(user => user.BirthDate, (faker, user) => faker.Person.DateOfBirth)
            .RuleFor(user => user.IsActive, (faker, user) => faker.Random.Bool())
            .RuleFor(user => user.RewardPoints, (faker, user) => faker.Random.Int(0, 99999));

        return generator.GenerateLazy(qty);
    }
    
    public static IEnumerable<RewardItem> GenerateRewardItems(int qty)
    {
        var generator = new Faker<RewardItem>()
            .RuleFor(item => item.Id, (faker, item) => faker.Random.Guid())
            .RuleFor(item => item.Name, (faker, item) => faker.Commerce.ProductName())
            .RuleFor(item => item.Cost, (faker, item) => faker.Random.Int(0, 99999))
            .RuleFor(item => item.IncludedAt, (faker, item) => faker.Date.Past().AddMonths(-6))
            .RuleFor(item => item.IsAvailable, (faker, user) => faker.Random.Bool())
            .RuleFor(item => item.MadeAvailableAt, (faker, item) => faker.Date.Past());

        return generator.GenerateLazy(qty);
    }
}