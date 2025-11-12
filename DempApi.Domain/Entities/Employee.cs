using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DempApi.Domain.Entities;

public class Employee : IBaseEntity
{
    // Base properties from IBaseEntity
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int InsertedById { get; set; }
    
    public int? UpdatedById { get; set; }
    
    [Required]
    public DateTime InsertedDate { get; set; }
    
    public DateTime? UpdatedDate { get; set; }
    
    [Required]
    public bool Deleted { get; set; } = false;

    // Employee-specific properties
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public DateTime HireDate { get; set; }
    
    // Foreign Keys
    [Required]
    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }
    
    [Required]
    [ForeignKey(nameof(Position))]
    public int PositionId { get; set; }
    
    // Navigation properties
    public Department Department { get; set; } = null!;
    public Position Position { get; set; } = null!;
}
