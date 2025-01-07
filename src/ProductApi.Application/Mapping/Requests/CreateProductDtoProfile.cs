using AutoMapper;
using ProductApi.Application.DTOs.Requests;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Mapping.Requests
{
    public class CreateProductDtoProfile : Profile
    {
        public CreateProductDtoProfile()
        {
            CreateMap<Product, CreateProductDto>()
                .ReverseMap();
        }
    }
}
