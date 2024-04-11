using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data.Models;
using ShoppingApi.Infrastructure.Interfaces;

namespace AdminPanel.Pages.Categories;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IEnumerable<Category> Categories;

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnGetAsync()
    {
        Categories = await _unitOfWork.Categories.GetAll();
    }
}