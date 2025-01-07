using FluentResults;
using MediatR;
using ProductApi.Application.DTOs.Responses;

namespace ProductApi.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;
}
