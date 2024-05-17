using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp_MVC.Models;
using PhotoApp_MVC.Repositories.IRepositories;
using PhotoApp_MVC.ViewModels;

namespace PhotoApp_MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        public UsersController(ApplicationDbContext context, 
            IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [Authorize]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync((int)id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                EmailAdress = user.EmailAdress,
                Password = user.Password,
                RoleName = user.Role.Name
            };

            return View(userViewModel);
        }

        [AllowAnonymous]
        // GET: Users/Create
        public async Task<IActionResult> CreateAsync()
        {
            var roleSelectList = _context.Roles.ToList().Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList();

            var userViewModel = new UserViewModel
            {
                Roles = roleSelectList
            };

            return View(userViewModel);
        }

        [AllowAnonymous]
        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,EmailAdress,Password,RoleName")] UserViewModel userViewModel)
        { 
            bool emailExists = await _context.Users.AnyAsync(u => u.EmailAdress == userViewModel.EmailAdress);
            if (emailExists)
            {
                ModelState.AddModelError("EmailAdress", "このメールアドレスは既に使用されています。");
            }

            Role role = _context.Roles.FirstOrDefault(r => r.Name == userViewModel.RoleName);

            if(role == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Name = userViewModel.Name,
                    Password = userViewModel.Password,
                    EmailAdress = userViewModel.EmailAdress,
                    Role = role,
                    RoleId = role.Id
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var roleSelectList = _context.Roles.ToList().Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList();

            userViewModel.Roles = roleSelectList;

            return View(userViewModel);
        }

        [Authorize]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }

            var roleSelectList = _context.Roles.ToList().Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList();

            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                EmailAdress = user.EmailAdress,
                Password = user.Password,
                RoleName = user.Role.Name
            };

            userViewModel.Roles = roleSelectList;

            return View(userViewModel);

        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Name,EmailAdress,Password,RoleName")] UserViewModel userViewModel)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.EmailAdress == userViewModel.EmailAdress);
            if (emailExists)
            {
                ModelState.AddModelError("EmailAdress", "このメールアドレスは既に使用されています。");
            }

            if (ModelState.IsValid)
            {
                User user = await _userRepository.GetUserByIdAsync((int)id);

                try
                {
                    user.Name = userViewModel.Name;
                    user.EmailAdress = userViewModel.EmailAdress;
                    user.Password = userViewModel.Password;

                    Role role = _context.Roles.FirstOrDefault(r => r.Name == userViewModel.RoleName);
                    user.Role = role;

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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

            var roleSelectList = _context.Roles.ToList().Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            }).ToList();

            userViewModel.Roles = roleSelectList;

            return View(userViewModel);
        }

        [Authorize]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync((int)id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                EmailAdress = user.EmailAdress,
                Password = user.Password,
                RoleName = user.Role.Name
            };

            return View(userViewModel);
        }

        [Authorize]
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userRepository.GetUserByIdAsync((int)id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
