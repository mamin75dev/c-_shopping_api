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

        return Ok(new ApiResponseDto<IEnumerable<Category>>
            { Status = ResponseStatus.Success, Message = "", Data = categories });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> SearchCategories([FromQuery] string name)
    {
        var categories = await _categoryService.SearchCategories(category => category.Name.Contains(name));

        return Ok(new ApiResponseDto<IEnumerable<Category>>
            { Status = ResponseStatus.Success, Message = "", Data = categories });
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
    public async Task<IActionResult> DeleteCategory(int catId)
    {
        var isDeleted = await _categoryService.DeleteCategory(catId);

        if (isDeleted)
            return Ok(new ApiResponseDto<object>
                { Status = ResponseStatus.Success, Message = "Category Deleted!" });


        return BadRequest(new ApiResponseDto<object>
            { Status = ResponseStatus.Success, Message = "Bad Request!" });
    }
}