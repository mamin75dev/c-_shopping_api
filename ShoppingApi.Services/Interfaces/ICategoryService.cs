using System.Linq.Expressions;
using ShoppingApi.Data.Models;

namespace ShoppingApi.Services.Interfaces;

public interface ICategoryService
{
    Task<bool> CreateCategory(Category category);
    Task<IEnumerable<Category>> GetAllCategories();
    Task<IEnumerable<Category>> SearchCategories(Expression<Func<Category, bool>> predicate);
    Task<bool> DeleteCategory(int catId);
}