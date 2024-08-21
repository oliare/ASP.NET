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

        CreateMap<Product, ProductItemViewModel>()
            .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages.Select(p => p.Image).ToArray()));

        CreateMap<Product, ProductEditViewModel>()
          .ForMember(x => x.Images, opt => 
          opt.MapFrom(src => src.ProductImages
          .Select(pi => new ProductImageViewModel
          {
              Id = pi.Id,
              Name = "/images/" + pi.Image,
              Priority = pi.Priotity
          }).ToList()));

        CreateMap<ProductEditViewModel, Product>()
            .ForMember(x => x.Id, opt => opt.Ignore());

    }
}
