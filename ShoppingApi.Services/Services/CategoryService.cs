using ShoppingApi.Data.Models;
using ShoppingApi.Infrastructure.Interfaces;
using ShoppingApi.Services.Interfaces;

namespace ShoppingApi.Services.Services;

public class CategoryService : ICategoryService
{
    public IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateCategory(Category category)
    {
        if (category != null)
        {
            await _unitOfWork.Categories.Add(category);

            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            return false;
        }

        return false;
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        var categoriesList = await _unitOfWork.Categories.GetAll();
        return categoriesList;
    }

    public async Task<bool> DeleteCategory(int catId)
    {
        if (catId > 0)
        {
            var category = await _unitOfWork.Categories.GetById(catId);
            if (category != null)
            {
                _unitOfWork.Categories.Delete(category);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                return false;
            }
        }

        return false;
    }
}