using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Manager
{
    public class DeleteCarModel : PageModel
    {
        private readonly ICarService _carService;

        public DeleteCarModel(ICarService carService)
        {
            _carService = carService;
        }
        public void OnGet(int id)
        {
            _carService.Delete(id);
        }
    }
}
