using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.Repositories;

namespace PhotoApp_MVC.Controllers
{
    [Authorize]
    public class PhotoPostsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public PhotoPostsController(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }


        // GET: PhotoPosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhotoPosts.ToListAsync());
        }

        // GET: PhotoPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoPost = await _context.PhotoPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photoPost == null)
            {
                return NotFound();
            }

            return View(photoPost);
        }

        // GET: PhotoPosts/Create
        public async Task<IActionResult> Create()
        {
            User user = await _userRepository.GetUserAsync(User);

            if (user.Categories != null)
            {
                ViewBag.CategoryList = new SelectList(user.Categories.ToList(), "Id", "Name");
            }
            return View();
        }

        // POST: PhotoPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,imageURL")] PhotoPost photoPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photoPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photoPost);
        }

        // GET: PhotoPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoPost = await _context.PhotoPosts.FindAsync(id);
            if (photoPost == null)
            {
                return NotFound();
            }
            return View(photoPost);
        }

        // POST: PhotoPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,imageURL")] PhotoPost photoPost)
        {
            if (id != photoPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photoPost == null)
            {
                return NotFound();
            }

            return View(photoPost);
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
