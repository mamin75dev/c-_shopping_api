using System.ComponentModel.DataAnnotations;

namespace ShoppingApi.Data.Dto.Request.Auth;

public class RegisterUserDto
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "وارد کردن نام الزامی است!")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است!")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = "وارد کردن شماره موبایل الزامی است!")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است!")]
    public string UserName { get; set; } = string.Empty;

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "وارد کردن رمز عبور الزامی است!")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "ادرس")]
    [Required(ErrorMessage = "وارد کردن آدرس الزامی است!")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "پست الکترونیکی")]
    [Required(ErrorMessage = "وارد کردن پست الکترونیکی الزامی است!")]
    [EmailAddress(ErrorMessage = "آدرس پست الکترونیکی نامعتبر است!")]
    public string Email { get; set; } = string.Empty;
}