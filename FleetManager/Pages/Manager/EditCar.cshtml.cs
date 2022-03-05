using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Pages.Manager
{
    public class EditCarModel : PageModel
    {
        private readonly ICarService _carService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogService _logService;

        [BindProperty]
        public Car Car { get; set; }


        public EditCarModel(ICarService carService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _carService = carService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }
        public void OnGet(int id)
        {
            Car = _carService.GetCar(id);
        }
        public void OnPost()
        {
            _carService.Edit(Car);
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _logService.Log(LogType.INFO, $"Upravil vozidlo s ID {Car.Id}", Convert.ToInt32(userId));
        }
    }
}
