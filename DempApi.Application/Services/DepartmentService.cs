using DempApi.Application.DTOs;
using DempApi.Application.Interfaces;
using DempApi.Domain.Entities;

namespace DempApi.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _departmentRepository;

    public DepartmentService(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();
        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Deleted = d.Deleted
        });
    }

    public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null) return null;

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            Deleted = department.Deleted
        };
    }

    public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDto)
    {
        var department = new Department
        {
            Name = createDto.Name,
            Description = createDto.Description,
            InsertedById = createDto.InsertedById,
            InsertedDate = DateTime.UtcNow,
            Deleted = false
        };

        var created = await _departmentRepository.AddAsync(department);

        return new DepartmentDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description,
            Deleted = created.Deleted
        };
    }

    public async Task UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDto)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
            throw new KeyNotFoundException($"Department with ID {id} not found");

        department.Name = updateDto.Name;
        department.Description = updateDto.Description;
        department.Deleted = updateDto.Deleted;
        department.UpdatedById = updateDto.UpdatedById;
        department.UpdatedDate = DateTime.UtcNow;

        await _departmentRepository.UpdateAsync(department);
    }

    public async Task DeleteDepartmentAsync(int id)
    {
        await _departmentRepository.DeleteAsync(id);
    }
}
