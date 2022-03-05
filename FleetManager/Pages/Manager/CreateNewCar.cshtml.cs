using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Pages.Manager
{
    public class CreateNewCarModel : PageModel
    {
        private readonly ICarService _carService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ILogService _logService;

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

        public CreateNewCarModel(ICarService carService, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, ILogService logService)
        {
            _carService = carService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _logService = logService;
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
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            _logService.Log(LogType.INFO, $"Vytvoøil nové vozidlo s SPZ {Input.Identificator} a VIN {Input.VIN}", Convert.ToInt32(userId));
            return Page();
        }
    }
}
