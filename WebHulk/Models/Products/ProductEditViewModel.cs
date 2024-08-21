using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebHulk.Models.Products
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public SelectList? CategoryList { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Choose a category")]
        public int CategoryId { get; set; }
        public List<string>? Images { get; set; }

        [Display(Name = "New images")]
        public List<IFormFile>? NewImages { get; set; }

    }
}
