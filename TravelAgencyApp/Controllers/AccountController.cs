//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using TravelAgencyApp.Models;
//using TravelAgencyApp.ViewModels;

//public class AccountController : Controller
//{
//    private readonly SignInManager<ApplicationUser> _signInManager;
//    private readonly UserManager<ApplicationUser> _userManager;

//    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
//    {
//        _signInManager = signInManager;
//        _userManager = userManager;
//    }

//    public IActionResult Login() => View();

//    [HttpPost]
//    public async Task<IActionResult> Login(LoginViewModel model)
//    {
//        if (!ModelState.IsValid) return View(model);

//        var user = await _userManager.FindByNameAsync(model.Username);
//        if (user != null)
//        {
//            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
//            if (result.Succeeded)
//            {
//                return RedirectToAction("Index", "Trips");
//            }
//        }

//        ModelState.AddModelError("", "Invalid username or password");
//        return View(model);
//    }

//    public async Task<IActionResult> Logout()
//    {
//        await _signInManager.SignOutAsync();
//        return RedirectToAction("Login");
//    }
//}
