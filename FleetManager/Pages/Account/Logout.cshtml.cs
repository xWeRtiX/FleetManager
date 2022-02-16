using FleetManager.Data;
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

        public LogoutModel(SignInManager<AppUser> signInManager)
        {
            this._signInManager = signInManager;

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
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl });
        }
    }
}
