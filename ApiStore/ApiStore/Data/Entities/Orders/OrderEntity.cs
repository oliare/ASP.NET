using ApiStore.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities.Orders;

public class OrderEntity
{
    [Key]
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }

    [ForeignKey("Status")]
    public int OrderStatusId { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("OrderItem")]
    public int OrderId { get; set; }
    public UserEntity? User { get; set; }
    public OrderStatusEntity? Status { get; set; }
    public List<OrderItemEntity> OrderItems { get; set; } = [];
}
