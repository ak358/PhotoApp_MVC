using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.Repositories.IRepositories;
using PhotoApp_MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoApp_MVC.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesByUserIdAsync(int userId)
        {
            return await _context.Categories
                .Include(c => c.PhotoPosts)
                .Include(c => c.User)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                    .Include(c => c.PhotoPosts)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                    .Include(c => c.PhotoPosts)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Name == name);
        }



        //public async Task AddCategoryAsync(Category category)
        //{
        //    _context.Categories.Add(category);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateCategoryAsync(Category category)
        //{
        //    _context.Entry(category).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteCategoryAsync(Category category)
        //{
        //    _context.Categories.Remove(category);
        //    await _context.SaveChangesAsync();
        //}
    }
}
