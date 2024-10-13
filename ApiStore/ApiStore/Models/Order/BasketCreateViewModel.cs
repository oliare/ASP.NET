namespace ApiStore.Models.Order;

public class BasketCreateViewModel
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public ushort Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
