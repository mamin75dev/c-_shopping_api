using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Data.Dto.Response;
using ShoppingApi.Data.Models;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Services.Interfaces;

namespace shopping.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    public readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategories();
        if (categories == null)
            return NotFound(new ApiResponseDto<object>
                { Status = ResponseStatus.Success, Message = "No categories found!" });

        return Ok(new ApiResponseDto<IEnumerable<Category>>
            { Status = ResponseStatus.Success, Message = "Categories Found!", Data = categories });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        var isCreated = await _categoryService.CreateCategory(category);

        if (isCreated)
            return Ok(new ApiResponseDto<object>
                { Status = ResponseStatus.Success, Message = "Category Created!" });

        return BadRequest(new ApiResponseDto<object>
            { Status = ResponseStatus.Success, Message = "Bad Request!" });
    }

    [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete]
    [Route("delete/{categoryId}")]
    public async Task<IActionResult> DeleteProduct(int catId)
    {
        var isDeleted = await _categoryService.DeleteCategory(catId);

        if (isDeleted)
            return Ok(new ApiResponseDto<object>
                { Status = ResponseStatus.Success, Message = "Category Deleted!" });


        return BadRequest(new ApiResponseDto<object>
            { Status = ResponseStatus.Success, Message = "Bad Request!" });
    }
}