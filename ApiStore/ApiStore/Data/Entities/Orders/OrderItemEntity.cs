using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities.Orders;

public class OrderItemEntity
{
    [Key]
    public int Id { get; set; }
    public decimal Price { get; set; }
    public ushort Quantity { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public ProductEntity? Product { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public OrderEntity? Order { get; set; }
}
