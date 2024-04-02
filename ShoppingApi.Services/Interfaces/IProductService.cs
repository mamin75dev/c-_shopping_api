using System.Linq.Expressions;
using ShoppingApi.Data.Dto.Request;
using ShoppingApi.Data.Models;

namespace ShoppingApi.Services.Interfaces;

public interface IProductService
{
    Task<bool> CreateProduct(CreateProductDto dto);
    Task<bool> UpdateProduct(int id, CreateProductDto dto);
    Task<Product> GetProductById(int id);
    Task<List<Product>> GetAllProducts();

    Task<List<Product>> SearchProducts(Expression<Func<Product, bool>> predicate);
    List<Product> GetAllProductsByCategoryId(int catId);

    Task<bool> DeleteProduct(int productId);
}