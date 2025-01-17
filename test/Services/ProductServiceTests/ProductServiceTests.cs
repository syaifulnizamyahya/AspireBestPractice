using AutoMapper;
using Moq;
using FluentAssertions;
using ProductApi.Application.DTOs.Requests;
using ProductApi.Application.DTOs.Responses;
using ProductApi.Domain.Entities;
using ProductApi.Application.Services;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProductDtos_WhenProductsExist()
        {
            // Arrange 
            var products = new List<Product>
            {
                new Product("Product1", 100),
                new Product("Product2", 200)
            };
            var productDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price)).ToList();

            _unitOfWorkMock.Setup(x => x.Products.GetAllAsync()).ReturnsAsync(products);
            _mapperMock.Setup(x => x.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

            // Act 
            var result = await _productService.GetAllProductsAsync();

            // Assert 
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull().And.HaveCount(2).And.BeEquivalentTo(productDtos);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProductDto_WhenProductExists()
        {
            // Arrange 
            var product = new Product("Product1", 100);
            var productDto = new ProductDto(product.Id, product.Name, product.Price);

            _unitOfWorkMock.Setup(x => x.Products.GetByIdAsync(product.Id)).ReturnsAsync(product);
            _mapperMock.Setup(x => x.Map<ProductDto>(product)).Returns(productDto);

            // Act 
            var result = await _productService.GetProductByIdAsync(product.Id);

            // Assert 
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(productDto);
        }

        [Fact]
        public async Task AddProductAsync_ShouldReturnProductId_WhenProductIsAdded()
        {
            // Arrange 
            var createProductDto = new CreateProductDto("Product1", 100);
            var product = new Product(createProductDto.Name, createProductDto.Price);

            _mapperMock.Setup(x => x.Map<Product>(createProductDto)).Returns(product);
            _unitOfWorkMock.Setup(x => x.Products.AddAsync(product)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act 
            var result = await _productService.AddProductAsync(createProductDto);

            // Assert 
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(product.Id);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldSucceed_WhenProductExists()
        {
            // Arrange
            var product = new Product("Product1", 100);
            var productId = product.Id;
            var updateProductDto = new UpdateProductDto("Product2", 200);

            _unitOfWorkMock.Setup(x => x.Products.GetByIdAsync(productId)).ReturnsAsync(product);
            _mapperMock.Setup(x => x.Map(updateProductDto, product))
                       .Callback<UpdateProductDto, Product>((dto, prod) =>
                       {
                           prod.Update(dto.Name, dto.Price);
                       });
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _productService.UpdateProductAsync(productId, updateProductDto);

            // Assert
            result.IsSuccess.Should().BeTrue();
            product.Name.Should().Be(updateProductDto.Name); 
            product.Price.Should().Be(updateProductDto.Price);
            _unitOfWorkMock.Verify(x => x.Products.GetByIdAsync(productId), Times.Once);
            _mapperMock.Verify(x => x.Map(updateProductDto, product), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldSucceed_WhenProductExists()
        {
            // Arrange 
            var product = new Product("Product1", 100);

            _unitOfWorkMock.Setup(x => x.Products.GetByIdAsync(product.Id)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(x => x.Products.DeleteAsync(product.Id)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act 
            var result = await _productService.DeleteProductAsync(product.Id);

            // Assert 
            result.IsSuccess.Should().BeTrue();
            _unitOfWorkMock.Verify(x => x.Products.DeleteAsync(product.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldFail_WhenProductDoesNotExist()
        {
            // Arrange 
            var productId = Guid.NewGuid();

            _unitOfWorkMock.Setup(x => x.Products.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act 
            var result = await _productService.DeleteProductAsync(productId);

            // Assert 
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == "Product not found");
        }
    }
}
