using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.Repositories.IRepositories;
using PhotoApp_MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoApp_MVC.Repositories
{
    public class PhotoPostRepository : IPhotoPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PhotoPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PhotoPost> GetPhotoPostByIdAsync(int id)
        {
            return await _context.PhotoPosts
                .Include(p => p.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PhotoPost>> GetPhotoPostsAsync()
        {
            return await _context.PhotoPosts
                .Include(p => p.User)
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}
