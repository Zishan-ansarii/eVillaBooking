using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Application.Utility;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Presentation.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVillaBooking.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IUnitOfWork unitOfWork,
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register(string returnUrl=null)
        {
            returnUrl ??= Url.Content("~/");

            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Customer)).Wait();
            }

            RegisterVM registerVM = new RegisterVM()
            {
                RoleList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }),
                RedirectUrl = returnUrl
            };
            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerVM.Email,
                Name = registerVM.Name,
                UserName = registerVM.Email,
                EmailConfirmed = true,
                //NormalizedEmail = registerVM.Email.ToUpper(),     CreateAsync isko khud hi handle karta hai
                PhoneNumber = registerVM.PhoneNumber,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(registerVM.Role))
                    await _userManager.AddToRoleAsync(user, registerVM.Role);
                else
                    await _userManager.AddToRoleAsync(user, StaticDetails.Role_Customer);

                await _signInManager.SignInAsync(user, isPersistent: false);

                return string.IsNullOrEmpty(registerVM.RedirectUrl)
                                ? RedirectToAction("Index", "Home")
                                : LocalRedirect(registerVM.RedirectUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            registerVM.RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            return View(registerVM);
        }
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new LoginVM()
            {
                RedirectUrl = returnUrl
            };
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email,
                                 loginVM.Password,
                                 isPersistent: loginVM.RememberMe,
                                 lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return string.IsNullOrEmpty(loginVM.RedirectUrl)
                                 ? RedirectToAction("Index", "Home")
                                 : LocalRedirect(loginVM.RedirectUrl);
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
