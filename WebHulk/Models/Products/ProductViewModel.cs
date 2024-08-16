using WebHulk.Data.Entities;

namespace WebHulk.Models.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public ICollection<ProductImage>? ProductImages { get; set; } 

    }
}
