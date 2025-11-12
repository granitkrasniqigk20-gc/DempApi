using DempApi.Application.Interfaces;
using DempApi.Application.Services;
using DempApi.Infrastructure.Data;
using DempApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPositionService, PositionService>();

var app = builder.Build();

// Database Migration and Update Configuration
// Comment/Uncomment the sections below as needed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    try
    {
        // OPTION 1: Delete database and recreate (for development/testing)
        // Uncomment to delete and recreate database on every startup
        // await context.Database.EnsureDeletedAsync();
        // await context.Database.EnsureCreatedAsync();
        
        // OPTION 2: Apply pending migrations automatically (Recommended for production)
        // Uncomment to automatically apply migrations
        await context.Database.MigrateAsync();
        
        // OPTION 3: Just ensure database exists (no migrations)
        // Uncomment to only create database if it doesn't exist
        // await context.Database.EnsureCreatedAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
