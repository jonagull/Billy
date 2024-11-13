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
builder.Services.AddDbContext<BillyContext>(opt => opt.UseSqlite("Data Source=data.db")); // Specify the SQLite database file path

builder.Services.AddHttpContextAccessor();

// builder
//     .Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.ReferenceHandler = System
//             .Text
//             .Json
//             .Serialization
//             .ReferenceHandler
//             .Preserve;
//         options.JsonSerializerOptions.MaxDepth = 64; // Increase the depth if necessary
//     });

// For local development uncomment the following line to use an in-memory database
// opt.UseInMemoryDatabase("GamesPlayedList"));

var app = builder.Build();

// Migrate database
await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetRequiredService<BillyContext>();

// Configure CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

// await db.Database.MigratedisAsync();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<TenantMiddleware>();

app.UseHttpsRedirection();
app.UseRouting(); // Add routing middleware

app.UseAuthorization();
app.MapControllers();
app.Run();
