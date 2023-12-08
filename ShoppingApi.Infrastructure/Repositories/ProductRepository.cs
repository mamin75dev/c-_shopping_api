using ShoppingApi.Data;
using ShoppingApi.Data.Models;
using ShoppingApi.Infrastructure.Interfaces;

namespace ShoppingApi.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context)
    {
    }
}