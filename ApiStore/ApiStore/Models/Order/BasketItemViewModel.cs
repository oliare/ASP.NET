namespace ApiStore.Models.Order;

public class BasketItemViewModel
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public uint Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}
