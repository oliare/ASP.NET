using ApiStore.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities.Orders;

public class BasketEntity
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public UserEntity? User { get; set; }
    public ProductEntity? Product { get; set; }
    public ushort Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
