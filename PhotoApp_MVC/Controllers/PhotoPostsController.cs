using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.Repositories;
using PhotoApp_MVC.Repositories.IRepositories;
using PhotoApp_MVC.ViewModels;

namespace PhotoApp_MVC.Controllers
{
    [Authorize]
    public class PhotoPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PhotoPostsController(ApplicationDbContext context,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }


        // GET: PhotoPosts
        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.GetUserByClaimsAsync(User);

            var photoPosts = await _context.PhotoPosts
                .Include(c => c.User)
                .Include(c => c.Category)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            return View(photoPosts);
        }

        // GET: PhotoPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoPost = await _context.PhotoPosts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (photoPost == null)
            {
                return NotFound();
            }

            var photoPostViewModel = new PhotoPostViewModel
            {
                Id = photoPost.Id,
                Title = photoPost.Title,
                Description = photoPost.Description,
                ImageUrl = photoPost.ImageUrl,
                CategoryName = photoPost.Category.Name,
                CreatedAt = photoPost.CreatedAt,
                UpdatedAt = photoPost.UpdatedAt
            };

            return View(photoPostViewModel);
        }

        // GET: PhotoPosts/Create
        public async Task<IActionResult> CreateAsync()
        {
            var user = await _userRepository.GetUserByClaimsAsync(User);

            var categories = _context.Categories
                .Where(c => c.UserId == user.Id)
                .ToList();

            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList();

            var photoPostViewModel = new PhotoPostViewModel
            {
                Categories = categorySelectList
            };

            return View(photoPostViewModel);

        }

        // POST: PhotoPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description ,ImageUrl,CategoryName")] PhotoPostViewModel photoPostViewModel)
        {
            var user = await _userRepository.GetUserByClaimsAsync(User);

            if (ModelState.IsValid)
            {

                var category = await _categoryRepository.GetCategoryByNameAsync(photoPostViewModel.CategoryName);

                if (category == null)
                {
                    return View(photoPostViewModel);
                }

                var CreatedTime = DateTime.Now;

                PhotoPost photoPost = new()
                {
                    Title = photoPostViewModel.Title,
                    Description = photoPostViewModel.Description,
                    ImageUrl = photoPostViewModel.ImageUrl,
                    Category = category,
                    User = user,
                    CategoryId = category.Id,
                    UserId = user.Id,
                    CreatedAt = CreatedTime,
                    UpdatedAt = CreatedTime
                };

                _context.Add(photoPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var categories = _context.Categories
                    .Where(c => c.UserId == user.Id)
                    .ToList();

                photoPostViewModel.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Name,
                    Text = c.Name
                }).ToList();

                return View(photoPostViewModel);
            }

        }

        // GET: PhotoPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByClaimsAsync(User);

            var categories = await _categoryRepository.GetCategoriesByUserIdAsync(user.Id);

            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList();

            var photoPost = await _context.PhotoPosts.FindAsync(id);
            if (photoPost == null)
            {
                return NotFound();
            }

            var photoPostViewModel = new PhotoPostViewModel
            {
                Id = photoPost.Id,
                Title = photoPost.Title,
                Description = photoPost.Description,
                ImageUrl = photoPost.ImageUrl,
                CategoryName = photoPost.Category.Name,
                CreatedAt = photoPost.CreatedAt,
                UpdatedAt = photoPost.UpdatedAt,
                Categories = categorySelectList
            };

            return View(photoPostViewModel);

        }

        // POST: PhotoPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,Title,Description,ImageUrl,CategoryName")] PhotoPostViewModel photoPostViewModel)
        {
            var photoPost = await _context.PhotoPosts
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (photoPost == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetCategoryByNameAsync(photoPostViewModel.CategoryName);
            
            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    photoPost.Title = photoPostViewModel.Title;
                    photoPost.Description = photoPostViewModel.Description;
                    photoPost.ImageUrl = photoPostViewModel.ImageUrl;
                    photoPost.Category = category;
                    photoPost.UpdatedAt = DateTime.Now;

                    _context.Update(photoPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoPostExists(photoPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(photoPost);
        }

        // GET: PhotoPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoPost = await _context.PhotoPosts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (photoPost == null)
            {
                return NotFound();
            }

            var photoPostViewModel = new PhotoPostViewModel
            {
                Id = photoPost.Id,
                Title = photoPost.Title,
                Description = photoPost.Description,
                ImageUrl = photoPost.ImageUrl,
                CategoryName = photoPost.Category.Name,
                CreatedAt = photoPost.CreatedAt,
                UpdatedAt = photoPost.UpdatedAt
            };

            return View(photoPostViewModel);
        }

        // POST: PhotoPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoPost = await _context.PhotoPosts.FindAsync(id);
            if (photoPost != null)
            {
                _context.PhotoPosts.Remove(photoPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoPostExists(int id)
        {
            return _context.PhotoPosts.Any(e => e.Id == id);
        }
    }
}
