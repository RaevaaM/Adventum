using Adventum.Areas.Account.Models;
using Adventum.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adventum.Areas.Account.Controllers;

[Area("Account")]
[Authorize(Roles = Constants.AdministratorRole)]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly AdventureContext _dbContext;


    public UserController(UserManager<User> userManager, AdventureContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    private async Task<string> FindRolesForAsync(string userId)
    {
        var result = await (from ur in _dbContext.UserRoles
            join r in _dbContext.Roles on ur.RoleId equals r.Id
            where ur.UserId == userId
            select r.Name).FirstAsync();

        return result;
    }

    public async Task<IActionResult> Index()
    {
        var userList = await _userManager.Users.Select(u => new UserModel
        {
            UserId = u.Id,
            Username = u.UserName,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
        }).ToListAsync();

        foreach (var user in userList)
        {
            user.RoleName = await FindRolesForAsync(user.UserId);
        }
        
        return View(userList);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(User user)
    {
        await _userManager.DeleteAsync(user);

        return RedirectToAction(nameof(Index));
    }
}