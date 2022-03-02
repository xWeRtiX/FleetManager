using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages
{
    public class ReservationsModel : PageModel
    {
        public List<Car> Cars { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime Date { get; set; } = DateTime.Now;

        private readonly ICarService _carService;

        public ReservationsModel(ICarService carService)
        {
            _carService = carService;
        }
        public void OnGet()
        {
            Cars = _carService.GetCars();
        }
    }
}
