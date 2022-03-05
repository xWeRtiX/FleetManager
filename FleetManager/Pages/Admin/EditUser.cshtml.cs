using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogService _logService;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }

            public string UserName { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
        }


        public EditUserModel(IUserService userService, UserManager<AppUser> userManager, ILogService logService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _userManager = userManager;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task OnGet(int id)
        {
            var user = await _userService.GetUser(id);
            Input = new InputModel { Id = user.Id, UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
        }

        public async Task OnPost()
        {
            var user = await _userService.GetUser(Input.Id);
            user.UserName = Input.UserName;
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Email = Input.Email;
            await _userService.Update(user);
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _logService.Log(LogType.INFO, $"Upravil uživatele s ID {Input.Id}", Convert.ToInt32(userId));
        }
    }
}
