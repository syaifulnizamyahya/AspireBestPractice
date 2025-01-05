namespace ProductApi.Application.DTOs.Responses
{
    public record ProductDto(Guid Id, string Name, decimal Price);

    //public class ProductDto
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }
    //}
}
