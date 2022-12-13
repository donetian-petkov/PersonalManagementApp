using HouseManagementApp.Core.Models.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace HouseManagementApp.Areas.Administration.Controllers;

[Area("Administration")]
[Authorize]
public class UserController : Controller
{
    private readonly UserManager<IdentityUser> userManager;

    private readonly SignInManager<IdentityUser> signInManager;

    public UserController(
        UserManager<IdentityUser> _userManager,
        SignInManager<IdentityUser> _signInManager)
    {
        userManager = _userManager;
        signInManager = _signInManager;
    }
    
    [HttpGet]
    [Route("/admin/register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User?.Identity?.IsAuthenticated ?? false)
        {
            return RedirectToAction("Index", "Home");
        }

        var model = new RegisterViewModel();

        return View(model);
    }

    [HttpPost]
    [Route("/admin/register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new IdentityUser()
        {
            Email = model.Email,
            UserName = model.UserName
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Login", "User");
        }

        foreach (var item in result.Errors)
        {
            ModelState.AddModelError("", item.Description);
        }

        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("/admin/login")]
    public IActionResult Login()
    {
        if (User?.Identity?.IsAuthenticated ?? false)
        {
            return RedirectToAction("Index", "Home");
        }

        var model = new LoginViewModel();

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/admin/login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.FindByNameAsync(model.UserName);

        if (user != null)
        {
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Administration");
            }
        }

        ModelState.AddModelError("", "Invalid login");

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}