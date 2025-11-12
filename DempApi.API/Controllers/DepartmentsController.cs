using DempApi.Application.DTOs;
using DempApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DempApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentsController> _logger;

    public DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger)
    {
        _departmentService = departmentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
    {
        try
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting departments");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
    {
        try
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound($"Department with ID {id} not found");

            return Ok(department);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting department {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> CreateDepartment(CreateDepartmentDto createDto)
    {
        try
        {
            var department = await _departmentService.CreateDepartmentAsync(createDto);
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating department");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDto updateDto)
    {
        try
        {
            await _departmentService.UpdateDepartmentAsync(id, updateDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating department {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        try
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting department {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
