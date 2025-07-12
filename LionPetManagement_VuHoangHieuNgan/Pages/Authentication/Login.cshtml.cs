using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LionPetManagement_VuHoangHieuNgan.Pages.Authentication
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly LionAccountService _lionAccountService;

        public LoginModel(LionAccountService lionAccountService)
        {
            _lionAccountService = lionAccountService;
        }
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _lionAccountService.GetUserAccountAsync(Email, Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email or Password!");
                TempData["Message"] = "Invalid Email or Password!";

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var identity = new ClaimsIdentity(claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync
                ((CookieAuthenticationDefaults.AuthenticationScheme),
                new ClaimsPrincipal(identity));

            Response.Cookies.Append("UserName", user.UserName);

            return RedirectToPage("/LionProfiles/Index");
        }
    }
}
