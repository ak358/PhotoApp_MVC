using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.Repositories.IRepositories;
using PhotoApp_MVC.ViewModels;
using System.Diagnostics;
using X.PagedList;

namespace PhotoApp_MVC.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IPhotoPostRepository _photoPostRepository;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            IPhotoPostRepository photoPostRepository)
        {
            _logger = logger;
            _context = context;
            _photoPostRepository = photoPostRepository;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var categories = GetCategoriesSelectList();

            var photoPostsViewModel = _context.PhotoPosts
                .OrderBy(p => p.UpdatedAt)
                .Select(p => new PhotoPostViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description.Length > 20 ? p.Description.Substring(0, 20) + "..." : p.Description,
                    ImageUrl = p.ImageUrl,
                    CategoryName = p.Category.Name,
                    UserName = p.User.Name,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    Categories = categories
                })
                .ToPagedList(pageNumber, pageSize);

            return View(photoPostsViewModel);
        }

        private List<SelectListItem> GetCategoriesSelectList()
        {
            var categories = _context.Categories.ToList();
            var selectListItems = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return selectListItems;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
