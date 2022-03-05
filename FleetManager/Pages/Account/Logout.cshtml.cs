using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogService _logService;

        public LogoutModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            this._logService = logService;

        }

        public async Task<SignOutResult> OnGetAsync()
        {
            var user = HttpContext.User;
            if (user?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
            }
            //await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            var callbackUrl = Url.Page("/Index", pageHandler: null, values: null, protocol: Request.Scheme);
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _logService.Log(LogType.INFO, $"Odhlásil se", Convert.ToInt32(userId));
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl });
        }
    }
}
