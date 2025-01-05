using FluentResults;
using MediatR;

namespace ProductApi.Application.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Result>;

    //public class DeleteProductCommand : IRequest<Result>
    //{
    //    public Guid Id { get; }

    //    public DeleteProductCommand(Guid id)
    //    {
    //        Id = id;
    //    }
    //}
}
