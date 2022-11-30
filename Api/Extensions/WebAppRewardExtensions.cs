using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Extensions;

public static class WebAppRewardExtensions
{
        /// <summary>
    /// This is basically a controller for the /reward endpoints. 
    /// </summary>
    /// <remarks>I believe that in .net7 this can be greatly improved and we can do more with less code.</remarks>
    /// <param name="app"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public static WebApplication MapRewardsEndpoints(this WebApplication app, string prefix = "/reward")
    {
        app.MapGet($"{prefix}", (IRewardsService rewardsService) => rewardsService.Get())
            .WithName("GetAllRewards")
            .WithDisplayName("Get All Rewards")
            .WithTags("RewardController")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary",
                description: "Returns a list with all rewards in database."))
            .Produces<IList<RewardItem>>()
            .Produces(StatusCodes.Status204NoContent);

        app.MapPost($"{prefix}", (IRewardsService rewardsService, [FromBody] RewardItem reward) =>
            {
                var insertedReward = rewardsService.Add(reward);
                return Results.Created(new Uri($"{prefix}/{insertedReward.Id}"), null);
            })
            .WithName("AddNewReward")
            .WithDisplayName("Add new reward")
            .WithTags("RewardController")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary",
                description: "Adds a new reward to the database."))
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status204NoContent);

        app.MapGet($"{prefix}/{{rewardId:guid}}", (IRewardsService rewardsService, Guid rewardId) =>
            {
                var reward = rewardsService.Get(rewardId);
                return Results.Ok(reward);
            })
            .WithName("GetSingleReward")
            .WithDisplayName("Get Single Reward")
            .WithTags("RewardController")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary",
                description: "Returns a single reward according to the Id passed."))
            .Produces<IList<User>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        app.MapGet($"{prefix}/eligible", (IRewardsService rewardsService, [FromQuery]int rewardPoints) =>
            {
                var reward = rewardsService.GetEligibleByPoints(rewardPoints);
                return Results.Ok(reward);
            })
            .WithName("GetEligibleRewards")
            .WithDisplayName("Get Eligible Rewards")
            .WithTags("RewardController")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary",
                description: "Returns a list of reward items according to the reward points."))
            .Produces<IList<User>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        return app;
    }
        
        
}