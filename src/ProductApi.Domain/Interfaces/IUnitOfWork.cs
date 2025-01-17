namespace ProductApi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        // Add any entity needs to be updated alongside Products here

        Task<int> SaveChangesAsync();
    }
}
