using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities;

[Table("Users")]
public class UserTable
{
    [Key] [MaxLength(100)] public string Id { get; set; } = string.Empty;

    [Required] [MaxLength(200)] public string Name { get; set; } = string.Empty;

    [Required] public DateTime CreatedAt { get; set; }

    // Navigation property for orders
    public virtual ICollection<OrderTable> Orders { get; set; } = new List<OrderTable>();
}