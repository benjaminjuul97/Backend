using FootballApplication.API.Middleware;
using FootballApplication.Model.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<
PlayerRepository, 
PlayerRepository>();

builder.Services.AddScoped<
ClubRepository, 
ClubRepository>();

builder.Services.AddScoped<
CountryRepository, 
CountryRepository>();

builder.Services.AddScoped<
ManagerRepository, 
ManagerRepository>();

builder.Services.AddScoped<
LeagueRepository, 
LeagueRepository>();

builder.Services.AddScoped<
TransferRepository, 
TransferRepository>();

builder.Services.AddScoped<
StadiumRepository, 
StadiumRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//app.UseHeaderAuthenticationMiddleware();
//app.UseBasicAuthenticationMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
