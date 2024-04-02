using ShoppingApi.Data.Dto.Request;
using ShoppingApi.Data.Models;

namespace ShoppingApi.Data.Mappers;

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
            EndOfDiscount = dto.EndOfDiscount,
            Colors = dto.Colors,
            Weight = dto.Weight,
            BodyMaterial = dto.BodyMaterial,
            Model = dto.Model,
            IntroductionDate = dto.IntroductionDate,
            Options = dto.Options,
            Images = dto.Images,
            CategoryId = dto.CategoryId,
            BrandId = dto.BrandId
        };

        return product;
    }

    public static Product MapToProduct(this CreateProductDto dto, int id)
    {
        if (id == null || id == 0) return null;
        if (dto.CategoryId == null) return null;

        var product = new Product
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Discount = dto.Discount,
            EndOfDiscount = dto.EndOfDiscount,
            Colors = dto.Colors,
            Weight = dto.Weight,
            BodyMaterial = dto.BodyMaterial,
            Model = dto.Model,
            IntroductionDate = dto.IntroductionDate,
            Options = dto.Options,
            Images = dto.Images,
            CategoryId = dto.CategoryId,
            BrandId = dto.BrandId
        };

        return product;
    }
}