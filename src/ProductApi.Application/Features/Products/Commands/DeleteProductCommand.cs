using FluentResults;
using MediatR;

namespace ProductApi.Application.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Result>;
}
