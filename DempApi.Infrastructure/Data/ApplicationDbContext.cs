using DempApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DempApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply global query filter for soft delete
        ApplyGlobalQueryFilters(modelBuilder);

        // Employee configuration - only relationships and what can't be done with attributes
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        // Apply global query filter for all entities implementing IBaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                var property = System.Linq.Expressions.Expression.Property(parameter, nameof(IBaseEntity.Deleted));
                var filter = System.Linq.Expressions.Expression.Lambda(
                    System.Linq.Expressions.Expression.Not(property), parameter);
                
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "IT", Description = "Information Technology", InsertedById = 1, InsertedDate = DateTime.UtcNow, Deleted = false },
            new Department { Id = 2, Name = "HR", Description = "Human Resources", InsertedById = 1, InsertedDate = DateTime.UtcNow, Deleted = false },
            new Department { Id = 3, Name = "Finance", Description = "Finance Department", InsertedById = 1, InsertedDate = DateTime.UtcNow, Deleted = false },
            new Department { Id = 4, Name = "Marketing", Description = "Marketing Department", InsertedById = 1, InsertedDate = DateTime.UtcNow.AddDays(-30) }
        );

        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Title = "Software Engineer", Description = "Develops software", InsertedById = 1, InsertedDate = DateTime.UtcNow, Deleted = false },
            new Position { Id = 2, Title = "Senior Software Engineer", Description = "Senior developer", InsertedById = 1, InsertedDate = DateTime.UtcNow, Deleted = false },
            new Position { Id = 3, Title = "HR Manager", Description = "Manages HR", InsertedById = 1, InsertedDate = DateTime.UtcNow, Deleted = false },
            new Position { Id = 4, Title = "Accountant", Description = "Manages finances", InsertedById = 1, InsertedDate = DateTime.UtcNow, Deleted = false },
            new Position { Id = 5, Title = "Marketing Manager", Description = "Manages marketing", InsertedById = 1, Deleted = true }
        );
    }
}
