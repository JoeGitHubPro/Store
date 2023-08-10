using AutoMapper;
using Store.DOTs;
using Store.Entities;
using Store.Identity.Models;

namespace Store.Mapping
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
