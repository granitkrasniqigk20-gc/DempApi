using DempApi.Application.DTOs;

namespace DempApi.Application.Interfaces;

public interface IPositionService
{
    Task<IEnumerable<PositionDto>> GetAllPositionsAsync();
    Task<PositionDto?> GetPositionByIdAsync(int id);
    Task<PositionDto> CreatePositionAsync(CreatePositionDto createDto);
    Task UpdatePositionAsync(int id, UpdatePositionDto updateDto);
    Task DeletePositionAsync(int id);
}
