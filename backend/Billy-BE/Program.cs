using Billy_BE.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(); // Add CORS service

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQLite database
builder.Services.AddDbContext<BillyContext>(opt =>
    opt.UseSqlite("Data Source=data.db")); // Specify the SQLite database file path
    // For local development uncomment the following line to use an in-memory database
    // opt.UseInMemoryDatabase("GamesPlayedList"));

var app = builder.Build();

// Migrate database
await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetRequiredService<BillyContext>();
await db.Database.MigrateAsync();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting(); // Add routing middleware
// Configure CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseAuthorization();
app.MapControllers();
app.Run();