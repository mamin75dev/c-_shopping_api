using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingApi.Data.Dto.Request.Auth;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ShoppingApi.Services.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AuthService(IConfiguration configuration, UserManager<User> manager) : base(configuration, manager)
    {
        _configuration = configuration;
        _userManager = manager;
    }

    public override async Task<string> GenerateJsonWebToken(User user)
    {
        var jwtKey = _configuration["JwtSettings:Key"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
            new(JwtRegisteredClaimNames.Jti, user.Id)
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var userRole in roles) claims.Add(new Claim(ClaimTypes.Role, userRole));


        var token = new JwtSecurityToken(
            // _configuration["JwtSettings:Issuer"],
            // _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public override async Task<User> AuthenticateUser(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password)) return user;

        return null;
    }
}