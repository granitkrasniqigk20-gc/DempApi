using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DempApi.Domain.Entities;

public interface IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    int Id { get; set; }

    [Required]
    int InsertedById { get; set; }

    int? UpdatedById { get; set; }

    [Required]
    DateTime InsertedDate { get; set; }

    DateTime? UpdatedDate { get; set; }

    [Required]
    bool Deleted { get; set; }
}
