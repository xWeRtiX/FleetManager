using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;

        public List<AppUser> Users { get; set; } = new();

        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            Users = _userService.GetUsers();
        }
    }
}
