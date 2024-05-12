using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using System.Security.Claims;

namespace PhotoApp_MVC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal userClaimsPrincipal)
        {
            var userIdClaim = userClaimsPrincipal.Claims.
                FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return null;
            }

            return await _context.Users
                .Include(u => u.Categories) // カテゴリを含める
                .Include(u=> u.PhotoPosts)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
