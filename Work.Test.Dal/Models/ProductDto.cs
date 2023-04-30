using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkTest.Dal.Models;

public class ProductDto
{
    [Key]
    public int Id { get; set; }

    public Guid OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public OrderDto Order { get; set; }
    [Required]
    public Guid ProdId { get; set; }
    public int Qty { get; set; }
}