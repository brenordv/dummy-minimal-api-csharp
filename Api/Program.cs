using Api.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Repositories.Repositories;
using Core.Services.Services;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.SetMinimumLevel(LogLevel.Trace);
    config.AddConsole();
});

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IRewardsRepository, RewardsRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRewardsService, RewardsService>();

// More at: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts => opts.EnableAnnotations());

var app = builder.Build();

app.MapExceptionHandler();

app.UsePathBase("/api");

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndpoints();

app.MapRewardsEndpoints();

app.Run();