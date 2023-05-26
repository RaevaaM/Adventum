using Adventum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Adventum.Areas.Account.Controllers;

[Area("Account")]
public class RegisterController : Controller
{
    private const string AdministratorRole = Constants.AdministratorRole;

    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IUserStore<User> _userStore;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterController(UserManager<User> userManager, IUserStore<User> userStore,
        SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userStore = userStore;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string username = "admin", string password = "Admin!00")
    {
        if (_userManager.Users.Any())
            return RedirectToAction(nameof(Index), "Home");

        var user = new User()
        {
            UserName = username,
            FirstName = Constants.AdministratorRole,
            LastName = "",
            Email = "admin@mail.com"
        };

        await _userStore.SetUserNameAsync(user, user.UserName, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return RedirectToAction(nameof(Index), "Home");

        await _roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));

        await _userManager.AddToRoleAsync(user, AdministratorRole);

        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction(nameof(Index), "Manage");
    }
}