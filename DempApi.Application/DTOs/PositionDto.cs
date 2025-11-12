namespace DempApi.Application.DTOs;

public class PositionDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Deleted { get; set; }
}

public class CreatePositionDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int InsertedById { get; set; }
}

public class UpdatePositionDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Deleted { get; set; }
    public int UpdatedById { get; set; }
}
