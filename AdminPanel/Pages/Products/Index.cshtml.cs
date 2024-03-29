using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data;
using ShoppingApi.Data.Models;

namespace AdminPanel.Pages.Products;

public class Index : PageModel
{
    private readonly DataContext _dbContext;

    public IEnumerable<Product> Products;

    public Index(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet()
    {
        Products = _dbContext.Products;
    }
}