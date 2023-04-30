using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WorkTest.Constants;

namespace WorkTest.Dal.Models;

[PrimaryKey("Id")]
public class OrderDto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public OrderStatus Status { get; set; }

    [Required]
    public DateTime Created { get; set; }

    public List<ProductDto> Lines { get; set; }

    public bool IsDeleted { get; set; }
}