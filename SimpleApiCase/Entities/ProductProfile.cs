using AutoMapper;

namespace SimpleApiCase.Entities
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddNewProductRequest, Product>()
                .ForMember(dest => dest.Id, options => options.Ignore());
            CreateMap<Product, GetAllProductsResponse>();
            CreateMap<Product, AddNewProductResponse>();
        }
    }
}
