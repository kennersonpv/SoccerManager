using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoccerManager.Api.Feature.Auth;
using SoccerManager.Api.Feature.Players.UpdatePlayer;
using SoccerManager.Api.Feature.Teams.UpdateTeam;
using SoccerManager.Api.Feature.Transfers.BuyPlayer;
using SoccerManager.Api.Feature.Transfers.SellPlayer;
using SoccerManager.Api.Feature.Transfers.TransferList;
using SoccerManager.Api.Feature.User.InsertUser;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Authentication;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.OptionsSetup;
using SoccerManager.Api.Shared.Repositories.Players;
using SoccerManager.Api.Shared.Repositories.PlayerValues;
using SoccerManager.Api.Shared.Repositories.TeamAmounts;
using SoccerManager.Api.Shared.Repositories.TeamPlayers;
using SoccerManager.Api.Shared.Repositories.Teams;
using SoccerManager.Api.Shared.Repositories.Transfers;
using SoccerManager.Api.Shared.Repositories.User;
using SoccerManager.Api.Shared.Repositories.Users;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPlayersRepository, PlayersRepository>();
builder.Services.AddScoped<IPlayerValueRepository, PlayerValueRepository>();
builder.Services.AddScoped<ITeamAmountRepository, TeamAmountRepository>();
builder.Services.AddScoped<ITeamPlayersRepository, TeamPlayersRepository>();
builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();

builder.Services.AddScoped<IInsertUserUseCase, InsertUserUseCase>();
builder.Services.AddScoped<IUpdatePlayerUseCase, UpdatePlayerUseCase>();
builder.Services.AddScoped<IUpdateTeamUseCase, UpdateTeamUseCase>();
builder.Services.AddScoped<IBuyPlayerUseCase, BuyPlayerUseCase>();
builder.Services.AddScoped<ISellPlayerUseCase, SellPlayerUseCase>();
builder.Services.AddScoped<ITransferListUseCase, TransferListUseCase>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();

var configuration = builder.Configuration;

var connectionString = configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<SoccerManagerDbContext>(options =>
options.UseNpgsql(connectionString)
);

var optionBuilder = new DbContextOptionsBuilder<SoccerManagerDbContext>();
optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
        };
    });
builder.Services.ConfigureOptions<JwtOptionsSetup>();
//builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
