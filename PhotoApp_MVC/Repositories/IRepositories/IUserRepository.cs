using PhotoApp_MVC.Models;
using System.Security.Claims;

namespace PhotoApp_MVC.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(ClaimsPrincipal userClaimsPrincipal);
    }
}
