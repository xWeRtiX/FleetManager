using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Admin
{
    public class UserModel : PageModel
    {
        private readonly IUserService _userService;

        private readonly ILogService _logService;

        public AppUser User { get; set; }

        public List<Log> Logs { get; set; }

        public UserModel(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        public async Task OnGet(int id)
        {
            User = await _userService.GetUser(id);
            Logs = _logService.GetUserLogs(id, 5);
        }
    }
}
