using DempApi.Application.DTOs;

namespace DempApi.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
    Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
    Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDto);
    Task UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDto);
    Task DeleteDepartmentAsync(int id);
}
