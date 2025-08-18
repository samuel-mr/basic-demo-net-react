using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities;

public enum OrderStatusTable
{
    Pending = 1,
    Processed = 2,
    Rejected = 3
}

[Table("Orders")]
public class OrderTable
{
    [Key] public Guid Id { get; set; }

    [Required] [MaxLength(100)] public string UserId { get; set; } = string.Empty;

    [Required] [Range(1, 1000000)] public int Quantity { get; set; }

    [Required] [MaxLength(20)] public OrderStatusTable Status { get; set; } = OrderStatusTable.Pending;

    [Required] public DateTime CreatedAt { get; set; }


    // Navigation properties can be added here if needed
    // public virtual UserEntity User { get; set; }
}