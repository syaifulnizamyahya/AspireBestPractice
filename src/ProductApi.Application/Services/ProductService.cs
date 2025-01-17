using AutoMapper;
using FluentResults;
using ProductApi.Application.DTOs.Requests;
using ProductApi.Application.DTOs.Responses;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Result.Ok(productDtos);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(Guid id)
        {
            var result = await FindProductByIdAsync(id);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            var productDto = _mapper.Map<ProductDto>(result.Value);

            return Result.Ok(productDto);
        }

        public async Task<Result<Guid>> AddProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(product.Id);
        }

        public async Task<Result> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var result = await FindProductByIdAsync(id);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            var product = result.Value;
            _mapper.Map(dto, product);

            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> DeleteProductAsync(Guid id)
        {
            var result = await FindProductByIdAsync(id);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private async Task<Result<Product>> FindProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                return Result.Fail("Product not found");

            return Result.Ok(product);
        }
    }
}
