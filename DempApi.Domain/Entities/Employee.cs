namespace DempApi.Domain.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Foreign Keys
    public int DepartmentId { get; set; }
    public int PositionId { get; set; }
    
    // Navigation properties
    public Department Department { get; set; } = null!;
    public Position Position { get; set; } = null!;
}
