using ProductApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace ProductApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<Result<ProductDto>> GetProductByIdAsync(Guid id);
        Task<Result> AddProductAsync(CreateProductDto dto);
        Task<Result> UpdateProductAsync(Guid id, UpdateProductDto dto);
        Task<Result> DeleteProductAsync(Guid id);
    }
}
