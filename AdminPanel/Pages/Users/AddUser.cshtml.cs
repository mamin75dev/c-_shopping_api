using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data.Dto.Request.Auth;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Infrastructure.Interfaces;

namespace AdminPanel.Pages.Users;

[BindProperties]
public class AddUser : PageModel
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public AddUser(IUnitOfWork unitOfWork, UserManager<User> manager,
        RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = manager;
        _roleManager = roleManager;
    }

    public RegisterUserDto User { get; set; }

    [Display(Name = "نقش کاربر")]
    [Required(ErrorMessage = "وارد کردن نقش کاربری الزامی است!")]
    public string UserRole { get; set; } = string.Empty;

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "وارد کردن رمز عبور الزامی است!")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "تکرار رمز عبور")]
    [Required(ErrorMessage = "وارد کردن تکرار رمز عبور الزامی است!")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public async Task OnGetAsync(string? id)
    {
        if (id != null)
        {
            var user = await _userManager.FindByIdAsync(id);
            User = new RegisterUserDto
            {
                Address = user.Address,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = "****",
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            Password = "****";
            ConfirmPassword = "****";
        }
        else
        {
            User = new RegisterUserDto();
        }
    }

    public async Task<IActionResult> OnPost()
    {
        if (Password == ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                var result = await RegisterUser(User, UserRole, Password);
                if (result == null)
                {
                    TempData["error"] = "User with this username exists!";
                    return Page();
                }

                if (!result.Succeeded)
                {
                    TempData["error"] = result.Errors.First().Description;
                    return Page();
                }

                TempData["success"] = "User created successfully!";
                return RedirectToPage("Index");
            }

            TempData["error"] = "Something went wrong!";
            return Page();
        }
        // // var result = _unitOfWork.Save();
        // TempData["success"] = "User created successfully!";
        // return RedirectToPage("Index");

        TempData["error"] = "Passwords do not match!";
        return Page();
    }

    private async Task<IdentityResult> RegisterUser(RegisterUserDto dto, string role, string pass)
    {
        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userExists != null) return null;

        User user = new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, pass);

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (role == UserRoles.Admin)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }

        return result;
    }
}