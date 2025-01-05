using FluentResults;
using MediatR;
using ProductApi.Application.DTOs.Responses;

namespace ProductApi.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;

    //public class GetProductByIdQuery : IRequest<Result<ProductDto>>
    //{
    //    public Guid Id { get; }

    //    public GetProductByIdQuery(Guid id)
    //    {
    //        Id = id;
    //    }
    //}
}
