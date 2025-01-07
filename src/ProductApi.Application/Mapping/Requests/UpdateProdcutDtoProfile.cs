using AutoMapper;
using ProductApi.Application.DTOs.Requests;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Mapping.Requests
{
    public class UpdateProdcutDtoProfile : Profile
    {
        public UpdateProdcutDtoProfile()
        {
            CreateMap<Product, UpdateProductDto>()
                .ReverseMap();
        }
    }
}
