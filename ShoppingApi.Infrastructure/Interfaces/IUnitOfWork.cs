namespace ShoppingApi.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }

    IUserRepository Users { get; }

    int Save();
}