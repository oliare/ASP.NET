using AutoMapper;
using WebHulk.DATA.Entities;
using WebHulk.Models.Categories;

public class AppMapProfile : Profile
{
    public AppMapProfile()
    {
        CreateMap<CategoryEntity, CategoryItemViewModel>();
        CreateMap<CategoryEntity, CategoryEditViewModel>();

    }
}
