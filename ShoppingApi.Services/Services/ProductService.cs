using System.Linq.Expressions;
using ShoppingApi.Data.Dto.Request;
using ShoppingApi.Data.Mappers;
using ShoppingApi.Data.Models;
using ShoppingApi.Infrastructure.Interfaces;
using ShoppingApi.Services.Interfaces;

namespace ShoppingApi.Services.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateProduct(CreateProductDto dto)
    {
        if (dto != null)
        {
            await _unitOfWork.Products.Add(dto.MapToProduct());
            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            return false;
        }

        return false;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = (await _unitOfWork.Products.GetAll()).ToList();
        return products;
    }

    public async Task<List<Product>> SearchProducts(Expression<Func<Product, bool>> predicate)
    {
        var products = (await _unitOfWork.Products.Search(predicate)).ToList();
        return products;
    }

    public List<Product> GetAllProductsByCategoryId(int catId)
    {
        var products = _unitOfWork.Products.GetMany(p => p.CategoryId == catId).ToList();
        return products;
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        if (productId > 0)
        {
            var product = await _unitOfWork.Products.GetById(productId);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                return false;
            }
        }

        return false;
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _unitOfWork.Products.GetById(id);
        return product;
    }

    public async Task<bool> UpdateProduct(int id, UpdateProductDto dto)
    {
        if (id <= 0) return false;

        if (dto != null)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if (product != null)
            {
                _unitOfWork.Products.Update(dto.MapToProduct(id));
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                return false;
            }

            return false;
        }

        return false;
    }
}