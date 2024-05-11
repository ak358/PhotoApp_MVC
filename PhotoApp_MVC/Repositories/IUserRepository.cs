using PhotoApp_MVC.Models;
using System.Security.Claims;

namespace PhotoApp_MVC.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(ClaimsPrincipal userClaimsPrincipal);
    }
}
