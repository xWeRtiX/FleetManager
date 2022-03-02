using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Manager
{
    public class DeleteNoteModel : PageModel
    {
        private readonly INoteService _noteService;

        public DeleteNoteModel(INoteService noteService)
        {
            _noteService = noteService;
        }

        public IActionResult OnGet(int id, int carId)
        {
            _noteService.Delete(id);
            return RedirectToPage("/Notes", new { carId });
        }
    }
}
