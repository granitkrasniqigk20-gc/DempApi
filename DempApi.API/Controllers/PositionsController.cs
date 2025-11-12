using DempApi.Application.DTOs;
using DempApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DempApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IPositionService _positionService;
    private readonly ILogger<PositionsController> _logger;

    public PositionsController(IPositionService positionService, ILogger<PositionsController> logger)
    {
        _positionService = positionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions()
    {
        try
        {
            var positions = await _positionService.GetAllPositionsAsync();
            return Ok(positions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting positions");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PositionDto>> GetPosition(int id)
    {
        try
        {
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null)
                return NotFound($"Position with ID {id} not found");

            return Ok(position);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting position {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<PositionDto>> CreatePosition(CreatePositionDto createDto)
    {
        try
        {
            var position = await _positionService.CreatePositionAsync(createDto);
            return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating position");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePosition(int id, UpdatePositionDto updateDto)
    {
        try
        {
            await _positionService.UpdatePositionAsync(id, updateDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating position {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePosition(int id)
    {
        try
        {
            await _positionService.DeletePositionAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting position {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
