using ShoppingApi.Data;
using ShoppingApi.Data.Models.Auth;
using ShoppingApi.Infrastructure.Interfaces;

namespace ShoppingApi.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}