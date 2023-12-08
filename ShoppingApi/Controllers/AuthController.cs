using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Data.Dto.Request.Auth;
using ShoppingApi.Data.Dto.Response;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Services.Interfaces;

namespace shopping.Controllers;

// token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhbWluc2hhcmlhdCIsInBob25lX251bWJlciI6IjA5MDM4NDUzMDc1IiwianRpIjoiOGViNGZlNWEtZjQ4Ni00YTYxLTg0ZTEtZDVjYTY3YmI2YmE3IiwiZXhwIjoxNzAyMDM0NDA2fQ.gu6YkPEq6pRlmJtcYfO9VqKjTX1ARZw8pAb2Qx1E10s

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public AuthController(IConfiguration configuration, IAuthService authService, UserManager<User> manager,
        RoleManager<IdentityRole> roleManager)
    {
        _configuration = configuration;
        _authService = authService;
        _userManager = manager;
        _roleManager = roleManager;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        IActionResult response = Unauthorized();

        var user = await _authService.AuthenticateUser(dto);

        if (user != null)
        {
            var tokenString = await _authService.GenerateJsonWebToken(user);
            response = Ok(new ApiResponseDto<object>
            {
                Status = ResponseStatus.Success,
                Message = "Successfull login!",
                Data = new { token = tokenString }
            });
        }

        return response;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var result = await RegisterUser(dto);

        if (result == null)
            return BadRequest(new ApiResponseDto<object> { Status = "Error", Message = "User already exists!" });

        if (!result.Succeeded)
            return BadRequest(
                new ApiResponseDto<object>
                {
                    Status = "Error", Message = "User creation failed! Please check user details and try again.",
                    Data = result.Errors
                });
        return Ok(new ApiResponseDto<object> { Status = "Success", Message = "User created successfully!" });
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto dto)
    {
        var result = await RegisterUser(dto);
        if (result == null)
            return BadRequest(new ApiResponseDto<object> { Status = "Error", Message = "User already exists!" });

        if (!result.Succeeded)
            return BadRequest(
                new ApiResponseDto<object>
                {
                    Status = "Error", Message = "User creation failed! Please check user details and try again.",
                    Data = result.Errors
                });


        return Ok(new ApiResponseDto<object> { Status = "Success", Message = "User created successfully!" });
    }

    private async Task<IdentityResult> RegisterUser(RegisterUserDto dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userExists != null)
            return null;

        User user = new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber
            // Password = dto.Password
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _userManager.AddToRoleAsync(user, UserRoles.User);

        return result;
    }
}