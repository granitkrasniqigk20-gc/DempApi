using DempApi.Application.DTOs;
using DempApi.Application.Interfaces;
using DempApi.Domain.Entities;

namespace DempApi.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Department> _departmentRepository;
    private readonly IRepository<Position> _positionRepository;

    public EmployeeService(
        IRepository<Employee> employeeRepository,
        IRepository<Department> departmentRepository,
        IRepository<Position> positionRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _positionRepository = positionRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            DateOfBirth = e.DateOfBirth,
            HireDate = e.HireDate,
            Deleted = e.Deleted,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department?.Name ?? string.Empty,
            PositionId = e.PositionId,
            PositionTitle = e.Position?.Title ?? string.Empty
        });
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null) return null;

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            DateOfBirth = employee.DateOfBirth,
            HireDate = employee.HireDate,
            Deleted = employee.Deleted,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name ?? string.Empty,
            PositionId = employee.PositionId,
            PositionTitle = employee.Position?.Title ?? string.Empty
        };
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto)
    {
        var employee = new Employee
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            PhoneNumber = createDto.PhoneNumber,
            DateOfBirth = createDto.DateOfBirth,
            HireDate = createDto.HireDate,
            DepartmentId = createDto.DepartmentId,
            PositionId = createDto.PositionId,
            InsertedById = createDto.InsertedById,
            InsertedDate = DateTime.UtcNow,
            Deleted = false
        };

        var created = await _employeeRepository.AddAsync(employee);
        var department = await _departmentRepository.GetByIdAsync(created.DepartmentId);
        var position = await _positionRepository.GetByIdAsync(created.PositionId);

        return new EmployeeDto
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            Email = created.Email,
            PhoneNumber = created.PhoneNumber,
            DateOfBirth = created.DateOfBirth,
            HireDate = created.HireDate,
            Deleted = created.Deleted,
            DepartmentId = created.DepartmentId,
            DepartmentName = department?.Name ?? string.Empty,
            PositionId = created.PositionId,
            PositionTitle = position?.Title ?? string.Empty
        };
    }

    public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDto updateDto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            throw new KeyNotFoundException($"Employee with ID {id} not found");

        employee.FirstName = updateDto.FirstName;
        employee.LastName = updateDto.LastName;
        employee.Email = updateDto.Email;
        employee.PhoneNumber = updateDto.PhoneNumber;
        employee.DateOfBirth = updateDto.DateOfBirth;
        employee.HireDate = updateDto.HireDate;
        employee.Deleted = updateDto.Deleted;
        employee.DepartmentId = updateDto.DepartmentId;
        employee.PositionId = updateDto.PositionId;
        employee.UpdatedById = updateDto.UpdatedById;
        employee.UpdatedDate = DateTime.UtcNow;

        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await _employeeRepository.DeleteAsync(id);
    }
}
