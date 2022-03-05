using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Manager
{
    public class DeleteNoteModel : PageModel
    {
        private readonly INoteService _noteService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogService _logService;

        public DeleteNoteModel(INoteService noteService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _noteService = noteService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        public IActionResult OnGet(int id, int carId)
        {
            _noteService.Delete(id);
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _logService.Log(LogType.INFO, $"Smazal poznámku ID {id} u vozidla s ID {carId}", Convert.ToInt32(userId));
            return RedirectToPage("/Notes", new { carId });
        }
    }
}
