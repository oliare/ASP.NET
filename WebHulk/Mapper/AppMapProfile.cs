using AutoMapper;
using System.Globalization;
using WebHulk.Data.Entities;
using WebHulk.Data.Entities.Identity;
using WebHulk.DATA.Entities;
using WebHulk.Models.Account;
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
          }).ToList()))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price.ToString(new CultureInfo("uk-UA"))));

        CreateMap<ProductEditViewModel, Product>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Price, opt => opt.MapFrom(x => Decimal.Parse(x.Price, new CultureInfo("uk-UA"))));


        CreateMap<UserEntity, ProfileViewModel>()
            .ForMember(x=>x.FullName, opt=>opt.MapFrom(x=>$"{x.FirstName} {x.LastName}"));
    }
}
