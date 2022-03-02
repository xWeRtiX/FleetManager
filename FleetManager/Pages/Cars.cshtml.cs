using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages
{
    public class CarsModel : PageModel
    {
        private readonly ICarService _carService;

        public List<Car> Cars { get; set; } = new();

        public CarsModel(ICarService carService)
        {
            _carService = carService;
        }
        public void OnGet()
        {
            Cars = _carService.GetCars();
        }
    }
}
