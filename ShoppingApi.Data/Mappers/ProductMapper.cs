using ShoppingApi.Data.Dto.Request;
using ShoppingApi.Data.Models;

namespace ShoppingApi.Data.Mappers
{
    public static class ProductMapper
    {

        public static Product MapToProduct(this CreateProductDto dto)
        {
            var product = new Product
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Discount = dto.Discount,
                Colors = dto.Colors,
                Sizes = dto.Sizes,
                Options = dto.Options,
                Images = dto.Images,
                CategoryId = dto.CategoryId,
            };

            return product;
        }

        public static Product MapToProduct(this UpdateProductDto dto, int id)
        {
            if (dto.CategoryId == null)
            {
                return null;
            }

            var product = new Product
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price ?? 0,
                Discount = dto.Discount ?? 0,
                Colors = dto.Colors,
                Sizes = dto.Sizes,
                Options = dto.Options,
                Images = dto.Images,
                CategoryId = dto.CategoryId ?? 1,
            };

            return product;
        }
    }
}
