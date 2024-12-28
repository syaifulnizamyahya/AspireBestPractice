using FluentResults;
using ProductApi.Application.DTOs;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

            return Result.Ok(productDtos);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Result.Fail("Product not found");

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return Result.Ok(productDto);
        }

        public async Task<Result> AddProductAsync(CreateProductDto dto)
        {
            var product = new Product(dto.Name, dto.Price);
            await _repository.AddAsync(product);
            return Result.Ok();
        }

        public async Task<Result> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Result.Fail("Product not found");

            product.Update(dto.Name, dto.Price);
            await _repository.UpdateAsync(product);
            return Result.Ok();
        }

        public async Task<Result> DeleteProductAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Result.Fail("Product not found");

            await _repository.DeleteAsync(id);
            return Result.Ok();
        }
    }
}
