using AutoMapper;
using FluentResults;
using ProductApi.Application.DTOs.Requests;
using ProductApi.Application.DTOs.Responses;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
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
            await _repository.AddAsync(product);
            return Result.Ok(product.Id);
        }

        public async Task<Result> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var result = await FindProductByIdAsync(id);
            if (result.IsFailed)
                return Result.Fail(result.Errors);

            var updatedProduct = _mapper.Map<Product>(dto);
            var product = result.Value;
            product.Update(updatedProduct);

            await _repository.UpdateAsync(product);

            return Result.Ok();
        }

        public async Task<Result> DeleteProductAsync(Guid id)
        {
            var result = await FindProductByIdAsync(id);
            if (result.IsFailed)
                return Result.Fail(result.Errors);

            await _repository.DeleteAsync(id);
            return Result.Ok();
        }

        private async Task<Result<Product>> FindProductByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Result.Fail("Product not found");

            return Result.Ok(product);
        }
    }
}
