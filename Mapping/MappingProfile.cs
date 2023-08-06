using AutoMapper;
using Store.Entities;

namespace Store.Mapping
{
    public class MappingProfile :Profile
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

        }
    }
}
