using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Pages.Manager
{
    public class EditCarModel : PageModel
    {
        private readonly ICarService _carService;

        [BindProperty]
        public Car Car { get; set; }


        public EditCarModel(ICarService carService)
        {
            _carService = carService;
        }
        public void OnGet(int id)
        {
            Car = _carService.GetCar(id);
        }
        public void OnPost()
        {
            _carService.Edit(Car);
        }
    }
}
