using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data;
using ShoppingApi.Data.Models;

namespace AdminPanel.Pages.Categories;

public class Index : PageModel
{
    private readonly DataContext _dbContext;

    public IEnumerable<Category> Categories;

    public Index(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet()
    {
        Categories = _dbContext.Categories;
    }
}