namespace DempApi.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
