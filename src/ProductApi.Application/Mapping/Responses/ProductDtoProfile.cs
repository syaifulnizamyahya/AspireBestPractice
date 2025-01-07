using AutoMapper;
using ProductApi.Application.DTOs.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Mapping.Responses
{
    public class ProductDtoProfile : Profile
    {
        public ProductDtoProfile()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}
