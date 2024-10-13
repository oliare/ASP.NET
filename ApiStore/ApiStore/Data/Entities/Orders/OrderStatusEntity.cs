namespace ApiStore.Data.Entities.Orders;

public class OrderStatusEntity
{
    public int Id { get; set; }
    public int Status { get; set; }
    public List<OrderEntity> Orders { get; set; } = [];
}
