using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Extensions;

public static class WebAppUserExtensions
{
    /// <summary>
    /// This is basically a controller for the /user endpoints. 
    /// </summary>
    /// <remarks>I believe that in .net7 this can be greatly improved and we can do more with less code.</remarks>
    /// <param name="app"></param>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public static WebApplication MapUserEndpoints(this WebApplication app, string prefix = "/user")
    {
        app.MapGet($"{prefix}", (IUserService userService) => userService.Get())
            .WithName("GetAllUsers")
            .WithDisplayName("Get All Users")
            .WithTags("UserController")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary",
                description: "Returns a list with all users in database."))
            .Produces<IList<User>>()
            .Produces(StatusCodes.Status204NoContent);

        app.MapPost($"{prefix}", (IUserService userService, [FromBody] User user) =>
            {
                userService.Add(user);
                return Results.Created(new Uri($"{prefix}/{user}"), null);
            })
            .WithName("AddNewUser")
            .WithDisplayName("Add new user")
            .WithTags("UserController")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary",
                description: "Adds a new user to the database."))
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status204NoContent);

        app.MapGet($"{prefix}/{{userId:guid}}", (IUserService userService, Guid userId) =>
            {
                var user = userService.Get(userId);
                return Results.Ok(user);
            })
            .WithName("GetSingleUser")
            .WithDisplayName("Get Single User")
            .WithTags("UserController")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Summary",
                description: "Returns a single user according to the Id passed."))
            .Produces<IList<User>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}