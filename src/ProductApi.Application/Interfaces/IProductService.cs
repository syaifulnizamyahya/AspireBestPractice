using FluentResults;
using ProductApi.Application.DTOs.Responses;
using ProductApi.Application.DTOs.Requests;

namespace ProductApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<Result<ProductDto>> GetProductByIdAsync(Guid id);
        Task<Result<Guid>> AddProductAsync(CreateProductDto dto);
        Task<Result> UpdateProductAsync(Guid id, UpdateProductDto dto);
        Task<Result> DeleteProductAsync(Guid id);
    }
}
