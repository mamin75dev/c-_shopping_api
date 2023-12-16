using ShoppingApi.Data.Dto.Request;
using ShoppingApi.Data.Models;

namespace ShoppingApi.Services.Interfaces;

public interface IProductService
{
    Task<bool> CreateProduct(CreateProductDto dto);
    Task<bool> UpdateProduct(int id, UpdateProductDto dto);
    Task<Product> GetProductById(int id);
    Task<List<Product>> GetAllProducts();
    List<Product> GetAllProductsByCategoryId(int catId);
}