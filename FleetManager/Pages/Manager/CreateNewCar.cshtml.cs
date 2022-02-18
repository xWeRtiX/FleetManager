using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Pages.Manager
{
    public class CreateNewCarModel : PageModel
    {
        private readonly ICarService _carService;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Brand { get; set; }

            [Required]
            public string Model { get; set; }

            public DateTime AssembledAt { get; set; }

            public DateTime TechnicalInspectionDate { get; set; }

            [Required]
            public string VIN { get; set; }

            [Required]
            public string Identificator { get; set; }

        }

        public CreateNewCarModel(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return StatusCode(500);
            _carService.Create(new Data.Car
            {
                Brand = Input.Brand,
                Model = Input.Model,
                AssembledAt = Input.AssembledAt,
                TechnicalInspectionDate = Input.TechnicalInspectionDate,
                VIN = Input.VIN,
                Identificator = Input.Identificator
            });
            return Page();
        }
    }
}
