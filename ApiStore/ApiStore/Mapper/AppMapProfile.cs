﻿using ApiStore.Data.Entities;
using ApiStore.Models.Category;
using AutoMapper;

namespace ApiStore.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryCreateViewModel, CategoryEntity>()
                .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<CategoryEditViewModel, CategoryEntity>()
                .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<CategoryEntity, CategoryItemViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore());

        }
    }
}
