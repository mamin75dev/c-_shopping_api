using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data;
using ShoppingApi.Data.Models.Auth;

namespace AdminPanel.Pages.Users;

public class Index : PageModel
{
    private readonly DataContext _dbContext;

    public IEnumerable<User> Users;

    public Index(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet()
    {
        Users = _dbContext.Users;
    }
}