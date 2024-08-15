using System.ComponentModel.DataAnnotations;
using WebHulk.DATA.Entities;

namespace WebHulk.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } 
        public ICollection<ProductImage> ProductImages { get; set; } 
    }

}
