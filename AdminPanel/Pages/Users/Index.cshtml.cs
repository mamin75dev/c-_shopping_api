using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Infrastructure.Interfaces;

namespace AdminPanel.Pages.Users;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public IEnumerable<User> Users;

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnGetAsync()
    {
        Users = await _unitOfWork.Users.GetAll();
    }
}