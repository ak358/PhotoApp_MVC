using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.Repositories.IRepositories;
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

        public async Task<IActionResult> Index()
        {
            var photoPosts = await _photoPostRepository.GetPhotoPostsAsync();
            //int? page
            //int pageSize = 9;
            //int pageNumber = (page ?? 1);

            //var photoPosts = _context.PhotoPosts
            //    .OrderBy(p => p.UpdatedAt)
            //    .ToPagedList(pageNumber, pageSize);

            return View(photoPosts);
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
