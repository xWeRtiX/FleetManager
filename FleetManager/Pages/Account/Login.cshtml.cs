using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogService _logService;

        public LoginModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            this._logService = logService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Required, DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = "/")
        {
            if (this.ModelState.IsValid)
            {
                var result = await this._signInManager.PasswordSignInAsync(
                    this.Input.UserName,
                    this.Input.Password,
                    this.Input.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.UserName);
                    _logService.Log(LogType.INFO, $"Pøihlásil se", Convert.ToInt32(user.Id));
                    return this.LocalRedirect(returnUrl);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Pøihlášení se nezdaøilo");
                }
            }
            return this.Page();
        }
    }
}
