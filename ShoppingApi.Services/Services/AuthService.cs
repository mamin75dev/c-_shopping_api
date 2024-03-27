using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingApi.Data.Dto.Request.Auth;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Services.Interfaces;
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

    public override async Task<JwtSecurityToken> GenerateJsonWebToken(User user)
    {
        var jwtKey = _configuration["JwtSettings:Key"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, user.UserName),
            // new(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
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

        return token;
    }

    public override async Task<User> AuthenticateUser(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password)) return user;

        return null;
    }

    public override string? GetPrincipalFromExpiredToken(string? token)
    {
        var jwtKey = _configuration["JwtSettings:Key"];
        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Name)?.Value;
    }

    public override string GenerateRefreshToke()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}