using ShoppingApi.Data;
using ShoppingApi.Data.Models;
using ShoppingApi.Infrastructure.Interfaces;

namespace ShoppingApi.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext context) : base(context)
    {
    }
}