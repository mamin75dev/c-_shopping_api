using Microsoft.AspNetCore.Identity;
using shopping.Dto;
using ShoppingApi.Data.Models;

namespace shopping.Services.AuthService;

public abstract class IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    protected IAuthService(IConfiguration configuration, UserManager<User> manager)
    {
        _configuration = configuration;
        _userManager = manager;
    }

    public abstract Task<string> GenerateJsonWebToken(User user);

    public abstract Task<User> AuthenticateUser(LoginDto dto);
}