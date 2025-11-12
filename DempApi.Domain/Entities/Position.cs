namespace DempApi.Domain.Entities;

public class Position : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
