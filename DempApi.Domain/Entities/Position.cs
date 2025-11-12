using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DempApi.Domain.Entities;

public class Position : IBaseEntity
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

    // Position-specific properties
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
