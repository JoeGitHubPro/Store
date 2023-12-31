﻿using AutoMapper;
using Store.Core.DOTs;
using Store.Core.Entities;
using Store.Core.Identity.Models;

namespace Store.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDTO>()
                .ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ReverseMap();

            CreateMap<Review, ReviewDTO>()
               .ReverseMap();

            CreateMap<Favorite, FavoriteDTO>()
               .ReverseMap();

            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(src => src.Id))
               .ReverseMap();

            CreateMap<Cart, CartDTO>()
               .ReverseMap(); 
            
            CreateMap<Order, OrderDTO>()
               .ReverseMap();
            
            CreateMap<OrderView, OrderViewDTO>()
               .ReverseMap();
            
            //CreateMap<Order, OrderViewDTO>()
            //   .ReverseMap();  
            
            CreateMap<CartDTO, OrderDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ReverseMap();
        }
    }
}
