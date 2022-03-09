using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages
{
    public class NotesModel : PageModel
    {
        public List<Note> Notes { get; set; } = new();

        [BindProperty]
        public InputModel Input { get; set; }

        private readonly INoteService _noteService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogService _logService;

        public class InputModel
        {
            public string Title { get; set; }

            public string Description { get; set; }

        }

        public NotesModel(INoteService noteService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _noteService = noteService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        public void OnGet(int carId)
        {
            Notes = _noteService.GetAll(carId);
        }

        public IActionResult OnPost(int carId)
        {
            if (Input.Title == null || Input.Description == null) return RedirectToPage("/Notes", new { carId });

            _noteService.Create(new Note { CarId = carId, Description = Input.Description, Title = Input.Title });
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _logService.Log(LogType.INFO, $"Zapsal poznámku k vozidlu ID: {carId} s titulem '{Input.Title}'", Convert.ToInt32(userId));
            return RedirectToPage("/Notes", new { carId });
        }
    }
}
