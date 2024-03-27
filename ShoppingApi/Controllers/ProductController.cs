using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Data.Dto.Request;
using ShoppingApi.Data.Dto.Response;
using ShoppingApi.Data.Models;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Services.Interfaces;

namespace shopping.Controllers;

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProducts();

        return Ok(new ApiResponseDto<IEnumerable<Product>>
            { Status = ResponseStatus.Success, Message = "", Data = products });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> SearchProducts([FromQuery] string name)
    {
        var products = await _productService.SearchProducts(product => product.Title.Contains(name));

        return Ok(new ApiResponseDto<IEnumerable<Product>>
            { Status = ResponseStatus.Success, Message = "", Data = products });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("")]
    public IActionResult GetAllProductsByCategoryId([FromQuery] int catId)
    {
        var products = _productService.GetAllProductsByCategoryId(catId);

        return Ok(new ApiResponseDto<IEnumerable<Product>>
            { Status = ResponseStatus.Success, Message = "", Data = products });
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        var isCreated = await _productService.CreateProduct(productDto);

        if (isCreated)
            return Ok(new ApiResponseDto<object>
                { Status = ResponseStatus.Success, Message = "Product Created!" });

        return BadRequest(new ApiResponseDto<object>
            { Status = ResponseStatus.Success, Message = "Bad Request!" });
    }

    [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete]
    [Route("delete/{productId}")]
    public async Task<IActionResult> DeleteProduct(int catId)
    {
        var isDeleted = await _productService.DeleteProduct(catId);

        if (isDeleted)
            return Ok(new ApiResponseDto<object>
                { Status = ResponseStatus.Success, Message = "Product Deleted!" });


        return BadRequest(new ApiResponseDto<object>
            { Status = ResponseStatus.Success, Message = "Bad Request!" });
    }
}