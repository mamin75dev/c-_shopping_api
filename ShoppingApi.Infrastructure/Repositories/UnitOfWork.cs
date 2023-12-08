using ShoppingApi.Data;
using ShoppingApi.Infrastructure.Interfaces;

namespace ShoppingApi.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(DataContext dbContext, ICategoryRepository categories, IProductRepository products)
    {
        _dbContext = dbContext;
        Categories = categories;
        Products = products;
    }

    public ICategoryRepository Categories { get; }
    public IProductRepository Products { get; }

    public int Save()
    {
        return _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) _dbContext.Dispose();
    }
}