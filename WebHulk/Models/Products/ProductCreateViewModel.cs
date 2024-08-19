namespace WebHulk.Models.Products;

public class ProductCreateViewModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string Images { get; set; } = string.Empty;
}