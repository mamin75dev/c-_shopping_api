using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data;
using ShoppingApi.Data.Models.Auth;

namespace AdminPanel.Pages.Users;

[BindProperties]
public class AddUser : PageModel
{
    private readonly DataContext _dbContext;

    public AddUser(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User User { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            await _dbContext.Users.AddAsync(User);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "User created successfully!";
            return RedirectToPage("Index");
        }

        TempData["error"] = "Something went wrong!";
        return Page();
    }
}