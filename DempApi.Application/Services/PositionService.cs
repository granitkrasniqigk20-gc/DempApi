using DempApi.Application.DTOs;
using DempApi.Application.Interfaces;
using DempApi.Domain.Entities;

namespace DempApi.Application.Services;

public class PositionService : IPositionService
{
    private readonly IRepository<Position> _positionRepository;

    public PositionService(IRepository<Position> positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IEnumerable<PositionDto>> GetAllPositionsAsync()
    {
        var positions = await _positionRepository.GetAllAsync();
        return positions.Select(p => new PositionDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            IsActive = p.IsActive
        });
    }

    public async Task<PositionDto?> GetPositionByIdAsync(int id)
    {
        var position = await _positionRepository.GetByIdAsync(id);
        if (position == null) return null;

        return new PositionDto
        {
            Id = position.Id,
            Title = position.Title,
            Description = position.Description,
            IsActive = position.IsActive
        };
    }

    public async Task<PositionDto> CreatePositionAsync(CreatePositionDto createDto)
    {
        var position = new Position
        {
            Title = createDto.Title,
            Description = createDto.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _positionRepository.AddAsync(position);

        return new PositionDto
        {
            Id = created.Id,
            Title = created.Title,
            Description = created.Description,
            IsActive = created.IsActive
        };
    }

    public async Task UpdatePositionAsync(int id, UpdatePositionDto updateDto)
    {
        var position = await _positionRepository.GetByIdAsync(id);
        if (position == null)
            throw new KeyNotFoundException($"Position with ID {id} not found");

        position.Title = updateDto.Title;
        position.Description = updateDto.Description;
        position.IsActive = updateDto.IsActive;
        position.UpdatedAt = DateTime.UtcNow;

        await _positionRepository.UpdateAsync(position);
    }

    public async Task DeletePositionAsync(int id)
    {
        await _positionRepository.DeleteAsync(id);
    }
}
