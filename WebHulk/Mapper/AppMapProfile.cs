using AutoMapper;
using WebHulk.Data.Entities;
using WebHulk.DATA.Entities;
using WebHulk.Models.Categories;
using WebHulk.Models.Products;

public class AppMapProfile : Profile
{
    public AppMapProfile()
    {
        CreateMap<CategoryEntity, CategoryItemViewModel>();
        CreateMap<CategoryEntity, CategoryEditViewModel>();
        CreateMap<Product, ProductViewModel>();
    }
}
