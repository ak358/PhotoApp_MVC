using PhotoApp_MVC.Models;
using System.Security.Claims;

namespace PhotoApp_MVC.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByClaimsAsync(ClaimsPrincipal userClaimsPrincipal);
        Task<User> GetUserByIdAsync(int id);

    }
}
