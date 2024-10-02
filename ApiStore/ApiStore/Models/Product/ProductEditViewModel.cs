namespace ApiStore.Models.Product;

public class ProductEditViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public List<ProductImages>? PreviousImages { get; set; }
    public List<ProductImages>? NewImages { get; set; }
}

public class ProductImages
{
    public IFormFile? Image { get; set; }
    public int Priority { get; set; }
}