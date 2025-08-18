using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities;

[Table("Products")]
public class ProductTable
{
    [Key] [MaxLength(100)] public string Id { get; set; } = string.Empty;

    [Required] [MaxLength(200)] public string Name { get; set; } = string.Empty;

    [Required] [Range(0, 10000)] public int StockQuantity { get; set; }

    [Required] public DateTime CreatedAt { get; set; }
}