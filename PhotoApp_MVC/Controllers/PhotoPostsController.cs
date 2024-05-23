using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Humanizer;
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
using X.PagedList;

namespace PhotoApp_MVC.Controllers
{
    [Authorize]
    public class PhotoPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoPostsController(ApplicationDbContext context,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: PhotoPosts
        public async Task<IActionResult> Index(int? page)
        {
            var user = await _userRepository.GetUserByClaimsAsync(User);

            var photoPosts = await _context.PhotoPosts
                .Include(c => c.User)
                .Include(c => c.Category)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            IPagedList<PhotoPost> photoPostsList = 
                photoPosts
                .OrderBy(p => p.UpdatedAt)
                .ToPagedList(pageNumber, pageSize);

            return View(photoPostsList);
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
                ImageUrl = "../../" + photoPost.ImageUrl,
                CategoryName = photoPost.Category.Name,
                CreatedAt = photoPost.CreatedAt,
                UpdatedAt = photoPost.UpdatedAt,
                UserId = photoPost.User.Id
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

            if (categories.Count == 0)
            {
                ViewData["message"] = "カテゴリが0件です。カテゴリを登録してください。";
            }

            return View(photoPostViewModel);

        }

        // POST: PhotoPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,CategoryName")] PhotoPostViewModel photoPostViewModel,
            IFormFile imageFile)
        {
            var user = await _userRepository.GetUserByClaimsAsync(User);
            photoPostViewModel.UserName = user.Name;

            if (ModelState.IsValid)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("images", uniqueFileName).Replace("\\", "/");


                var category = await _categoryRepository
                    .GetCategoryByNameAsync(photoPostViewModel.CategoryName);

                if (category == null)
                {
                    return View(photoPostViewModel);
                }

                var createdTime = DateTime.Now;

                PhotoPost photoPost = new()
                {
                    Title = photoPostViewModel.Title,
                    Description = photoPostViewModel.Description,
                    ImageUrl = relativePath,
                    Category = category,
                    User = user,
                    CategoryId = category.Id,
                    UserId = user.Id,
                    CreatedAt = createdTime,
                    UpdatedAt = createdTime
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

            var photoPost = await _context.PhotoPosts
                .Include(c => c.Category)
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
                ImageUrl = "../../" + photoPost.ImageUrl,
                UserName = photoPost.User.Name,
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
            [Bind("Id,Title,Description,ImageUrl,CategoryName")] PhotoPostViewModel photoPostViewModel,
            IFormFile imageFile)
        {
            var photoPost = await _context.PhotoPosts
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (photoPost == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository
                .GetCategoryByNameAsync(photoPostViewModel.CategoryName);

            if (category == null)
            {
                return NotFound();
            }

            if (imageFile == null)
            {
                ModelState.Remove("imageFile");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null)
                    {
                        DeleteImage(photoPost.ImageUrl);

                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        var relativePath = Path.Combine("images", uniqueFileName).Replace("\\", "/");
                        photoPost.ImageUrl = relativePath;
                    }

                    photoPost.Title = photoPostViewModel.Title;
                    photoPost.Description = photoPostViewModel.Description;
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

        /// <summary>
        /// 画像を削除するメソッドを追加
        /// </summary>
        /// <param name="imageUrl"></param>
        private void DeleteImage(string imageUrl)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
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
                ImageUrl = "../../" + photoPost.ImageUrl,
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
            var photoPost = await _context.PhotoPosts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

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
