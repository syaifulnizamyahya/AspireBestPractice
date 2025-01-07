using FluentResults;
using MediatR;
using ProductApi.Application.DTOs.Responses;

namespace ProductApi.Application.Features.Products.Queries
{
    public record GetAllProductsQuery : IRequest<Result<IEnumerable<ProductDto>>>;
}
