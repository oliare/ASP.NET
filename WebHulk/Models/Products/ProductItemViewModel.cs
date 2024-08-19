using WebHulk.Data.Entities;

namespace WebHulk.Models.Products
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = String.Empty;
        public ICollection<ProductImage>? ProductImages { get; set; } 

    }
}
