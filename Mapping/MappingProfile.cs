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
        }
    }
}
