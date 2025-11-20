using MyPIMApi.Data;
using MyPIMApi.Repositories;
using Microsoft.EntityFrameworkCore; 
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ********************************************************************
// SERVICE INJECTION (DbContext and Repository)
// ********************************************************************

// 1. DbContext Configuration using SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString)); 

// 2. Add the Repository (Dependency Injection)
builder.Services.AddScoped<PlaylistRepository>();

// Standard ASP.NET Core Services
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ********************************************************************
// PIPELINE CONFIGURATION AND DATABASE INITIALIZATION
// ********************************************************************

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Database Initialization: Creates the SQLite file and schema if it does not exist.
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
    }
}

// Ensure HttpsRedirection is commented out for development/HTTP access.
// app.UseHttpsRedirection(); 

app.UseAuthorization();

app.MapControllers();

app.Run();