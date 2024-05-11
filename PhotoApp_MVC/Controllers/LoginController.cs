using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // ログインフォームを表示するためのアクション
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // ログインを処理するためのアクション
        [HttpPost]
        public async Task<IActionResult> Index(string username, string password, string returnUrl)
        {
            // ログインを試行する
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // ログイン成功した場合、リダイレクトする
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                // ログイン失敗した場合、再度ログインフォームを表示する
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        // ログアウトを処理するためのアクション
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // ログアウトする
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
