using FluentResults;
using MediatR;
using ProductApi.Application.DTOs.Requests;

namespace ProductApi.Application.Features.Products.Commands
{
    public record UpdateProductCommand(Guid Id, UpdateProductDto Dto) : IRequest<Result>;

    //public class UpdateProductCommand : IRequest<Result>
    //{
    //    public Guid Id { get; }
    //    public UpdateProductDto Dto { get; }

    //    public UpdateProductCommand(Guid id, UpdateProductDto dto)
    //    {
    //        Id = id;
    //        Dto = dto;
    //    }
    //}
}
