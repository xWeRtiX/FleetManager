using FleetManager.Data;
using FleetManager.Services.Interfaces;
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

        public class InputModel
        {
            public string Title { get; set; }

            public string Description { get; set; }

        }

        public NotesModel(INoteService noteService)
        {
            _noteService = noteService;
        }

        public void OnGet(int carId)
        {
            Notes = _noteService.GetAll(carId);
        }

        public IActionResult OnPost(int carId)
        {
            if (Input.Title == null || Input.Description == null) return RedirectToPage("/Notes", new { carId });

            _noteService.Create(new Note { CarId = carId, Description = Input.Description, Title = Input.Title });
            return RedirectToPage("/Notes", new { carId });
        }
    }
}
