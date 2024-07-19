using System.ComponentModel.DataAnnotations;

namespace WebHulk.Models.Categories
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; } = string.Empty;
    }
}
