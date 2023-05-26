using Adventum.Data;
using Adventum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Adventum.Controllers;

public class LoginController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IUserStore<User> _userStore;
    private readonly IUserEmailStore<User> _emailStore;
    private readonly RoleManager<IdentityRole> _roleManager;


    public LoginController(UserManager<User> userManager, IUserStore<User> userStore,
        SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userStore = userStore;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _emailStore = GetEmailStore();
    }

    [HttpGet]
    public async Task<IActionResult> Index() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
                return LocalRedirect("/Account/Manage");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new User
        {
            UserName = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };

        await _userStore.SetUserNameAsync(user, user.UserName, CancellationToken.None);
        
        if(model.Email.Length > 1 && model.Email.Contains("@"))
            await _emailStore.SetEmailAsync(user, user.Email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var defaultRole = await _roleManager.FindByNameAsync(Constants.DefaultUserRole);
            if (defaultRole == null)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(Constants.DefaultUserRole));

                if (roleResult.Succeeded == false)
                {
                    ModelState.AddModelError(string.Empty, "Could not create default role.");
                    return View(model);
                }
            }
            
            await _userManager.AddToRoleAsync(user, Constants.DefaultUserRole);
            await _signInManager.SignInAsync(user, isPersistent: false);
            
            return RedirectToAction(nameof(Index), "Home");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    private IUserEmailStore<User> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email support.");

        return (IUserEmailStore<User>)_userStore;
    }
}