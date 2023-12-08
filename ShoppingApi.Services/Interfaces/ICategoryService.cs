using ShoppingApi.Data.Models;

namespace ShoppingApi.Services.Interfaces;

public interface ICategoryService
{
    Task<bool> CreateCategory(Category category);
    Task<IEnumerable<Category>> GetAllCategories();

    Task<bool> DeleteCategory(int catId);
}