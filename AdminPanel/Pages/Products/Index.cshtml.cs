using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data.Models;
using ShoppingApi.Infrastructure.Interfaces;

namespace AdminPanel.Pages.Products;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IEnumerable<Product> Products;

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnGetAsync(int? catId)
    {
        if (catId != null) Products = _unitOfWork.Products.GetMany(p => p.CategoryId == catId);
        else Products = await _unitOfWork.Products.GetAll();
    }
}