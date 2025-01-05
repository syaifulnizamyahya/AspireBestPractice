using FluentResults;
using MediatR;
using ProductApi.Application.Interfaces;

namespace ProductApi.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.AddProductAsync(request.Dto);
        }
    }
}
