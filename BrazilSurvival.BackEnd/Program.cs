using BrazilSurvival.BackEnd.Repos;

IChallengeRepo challengeRepo = new StaticChallengeRepo();
IPlayerScoreRepo playerScoreRepo = new StaticPlayersRepo();

string[] allowedOrigins =
{
    "https://app.brazilsurvival:3535",
    "http://localhost:5173"
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy => policy
        .AllowAnyOrigin()
        .WithMethods("DELETE", "UPDATE")
        .AllowAnyHeader()));

builder.Services.AddControllers();

builder.Services.AddSingleton<IPlayerScoreRepo, StaticPlayersRepo>();
builder.Services.AddScoped<IChallengeRepo, StaticChallengeRepo>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

var group = app.MapGroup("/api");

group.MapPost("/login", async () =>
{
    return await Task.FromResult("TOKEN");
});

app.MapControllers();

app.Run("http://localhost:5000");