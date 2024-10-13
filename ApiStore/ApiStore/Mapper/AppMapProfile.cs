﻿using ApiStore.Data.Entities;
using ApiStore.Data.Entities.Identity;
using ApiStore.Data.Entities.Orders;
using ApiStore.Models.Account;
using ApiStore.Models.Category;
using ApiStore.Models.Order;
using ApiStore.Models.Product;
using AutoMapper;

namespace ApiStore.Mapper;

public class AppMapProfile : Profile
{
    public AppMapProfile()
    {
        // Categories
        CreateMap<CategoryCreateViewModel, CategoryEntity>()
           .ForMember(x => x.Image, opt => opt.Ignore());

        CreateMap<CategoryEditViewModel, CategoryEntity>()
           .ForMember(x => x.Image, opt => opt.Ignore());

        CreateMap<CategoryEntity, CategoryItemViewModel>();

        CreateMap<CategoryEntity, SelectItemViewModel>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        // Products
        CreateMap<ProductEntity, ProductItemViewModel>()
           .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages
           .Select(p => p.Image).ToArray()));

        CreateMap<ProductCreateViewModel, ProductEntity>();
        CreateMap<ProductEditViewModel, ProductEntity>()
           .ForMember(x => x.ProductImages, opt => opt.Ignore());

        // Auth
        CreateMap<RegisterViewModel, UserEntity>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Image, opt => opt.Ignore());

        // Basket
        CreateMap<BasketEntity, BasketItemViewModel>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

    }
}