using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewData["returnUrl"]=returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login,string? returnUrl)
        {
            if (ModelState.IsValid) { 
                var result=await _signinManager.PasswordSignInAsync(login.Email,login.Password,login.RememberMe,lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    if(_signinManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return Redirect("/Admin/Home/Index");
                    }
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Gmail Hoặc Số Điện Thoại Không Đúng");
            return View(login);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid) {
                var user = new AppUser()
                {
                    UserName = register.Email,
                    Email = register.Email,
                    FullName=register.FullName,
                };
                //return Json(user);
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Customer"))
                    {
                        var createRole = await _roleManager.CreateAsync(new IdentityRole("Customer"));
                        if (createRole.Succeeded)
                        {
                            var role = await _userManager.AddToRoleAsync(user, "Customer");
                            await _signinManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        var role = await _userManager.AddToRoleAsync(user, "Customer");
                        await _signinManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(register);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user=await _userManager.GetUserAsync(User);
            var Profile = new ProfileViewModel()
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = ""
            };
            return View(Profile);
        }
    }
}
