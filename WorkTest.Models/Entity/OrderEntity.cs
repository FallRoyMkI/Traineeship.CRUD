using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WorkTest.Models.Enum;

namespace WorkTest.Models.Entity;

[PrimaryKey("Id")]
public class OrderEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public OrderStatus Status { get; set; }

    [Required]
    public DateTime Created { get; set; }

    public List<OrderLineEntity> Lines { get; set; }

    public bool IsDeleted { get; set; }
}