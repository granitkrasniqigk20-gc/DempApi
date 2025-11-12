using DempApi.Application.Interfaces;
using DempApi.Domain.Entities;
using DempApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DempApi.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        if (typeof(T) == typeof(Employee))
        {
            return (IEnumerable<T>)await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .ToListAsync();
        }
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        if (typeof(T) == typeof(Employee))
        {
            return (T?)(object?)await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        // Reload with navigation properties for Employee
        if (typeof(T) == typeof(Employee) && entity is Employee employee)
        {
            return (T)(object)(await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstAsync(e => e.Id == employee.Id));
        }
        
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
