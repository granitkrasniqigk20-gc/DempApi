using DempApi.Application.DTOs;

namespace DempApi.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto);
    Task UpdateEmployeeAsync(int id, UpdateEmployeeDto updateDto);
    Task DeleteEmployeeAsync(int id);
}
