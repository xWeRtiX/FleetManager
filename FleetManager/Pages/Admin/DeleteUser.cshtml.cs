using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Admin
{
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogService _logService;

        public DeleteUserModel(IUserService userService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _userService = userService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }
        public void OnGet(int id)
        {
            _userService.Delete(id);
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _logService.Log(LogType.INFO, $"Smazal uživatele s ID {id}", Convert.ToInt32(userId));
        }
    }
}
