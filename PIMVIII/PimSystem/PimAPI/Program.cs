using MyPIMApi.Data;
using MyPIMApi.Repositories;
using Microsoft.EntityFrameworkCore; 
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ********************************************************************
// SERVICE INJECTION (DbContext and Repository)
// ********************************************************************

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString)); 

builder.Services.AddScoped<PlaylistRepository>();

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
    
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();