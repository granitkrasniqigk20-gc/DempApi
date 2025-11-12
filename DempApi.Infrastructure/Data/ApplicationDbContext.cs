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

        // Employee configuration
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Department configuration
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(200);
            entity.Property(d => d.Description).HasMaxLength(500);
        });

        // Position configuration
        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasMaxLength(500);
        });

        // Seed data
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "IT", Description = "Information Technology", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Department { Id = 2, Name = "HR", Description = "Human Resources", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Department { Id = 3, Name = "Finance", Description = "Finance Department", IsActive = true, CreatedAt = DateTime.UtcNow }
        );

        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Title = "Software Engineer", Description = "Develops software", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Position { Id = 2, Title = "Senior Software Engineer", Description = "Senior developer", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Position { Id = 3, Title = "HR Manager", Description = "Manages HR", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Position { Id = 4, Title = "Accountant", Description = "Manages finances", IsActive = true, CreatedAt = DateTime.UtcNow }
        );
    }
}
