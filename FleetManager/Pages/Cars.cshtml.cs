using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages
{
    public class CarsModel : PageModel
    {
        private readonly ICarService _carService;

        private readonly ICarReservationService _carReservationService;

        public List<Car> Cars { get; set; } = new();

        public List<CarReservation> CarsReservation { get; set; } = new();

        public CarsModel(ICarService carService, ICarReservationService carReservationService)
        {
            _carService = carService;
            _carReservationService = carReservationService;
        }
        public void OnGet()
        {
            Cars = _carService.GetCars();
            CarsReservation = _carReservationService.GetAll();
        }
    }
}
