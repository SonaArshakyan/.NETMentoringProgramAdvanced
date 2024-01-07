using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagmentServer.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(//)
        SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);

        if (result.Succeeded)
        {
            return Redirect(vm.ReturnUrl);
        }
        return View();
    }
}
