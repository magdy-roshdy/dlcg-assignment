using GameCatalogue.Api.Extensions;
using GameCatalogue.Application.CQRS.Queries;
using GameCatalogue.Application.Interfaces;
using GameCatalogue.Application.Mapping;
using GameCatalogue.Application.Services;
using GameCatalogue.Domain.Interfaces;
using GameCatalogue.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
  opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg =>
  cfg.RegisterServicesFromAssemblies(typeof(GetGamesQuery).Assembly)
);
builder.Services.AddAutoMapper(typeof(DomainToDtoProfile).Assembly);

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IFileStorageService, WebFileStorageService>();

builder.Services.AddCors(opts =>
  opts.AddPolicy("AllowAll", p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseStaticFiles();
app.UseCors("AllowAll");
app.MapControllers();

//make sure to apply any pending migrations, and seeding data if needed
try { app.ApplyMigrationsAndSeed(app.Environment.IsDevelopment()); } catch (Exception ex) { }

app.Run();

