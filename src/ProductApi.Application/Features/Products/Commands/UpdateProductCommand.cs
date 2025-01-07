using FluentResults;
using MediatR;
using ProductApi.Application.DTOs.Requests;

namespace ProductApi.Application.Features.Products.Commands
{
    public record UpdateProductCommand(Guid Id, UpdateProductDto Dto) : IRequest<Result>;
}
