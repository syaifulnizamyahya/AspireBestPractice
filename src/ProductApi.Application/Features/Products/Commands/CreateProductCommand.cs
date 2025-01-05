using FluentResults;
using MediatR;
using ProductApi.Application.DTOs.Requests;

namespace ProductApi.Application.Features.Products.Commands
{
    public record CreateProductCommand(CreateProductDto Dto) : IRequest<Result<Guid>>;

    //public class CreateProductCommand : IRequest<Result<Guid>>
    //{
    //    public CreateProductDto Dto { get; }

    //    public CreateProductCommand(CreateProductDto dto)
    //    {
    //        Dto = dto;
    //    }
    //}
}
