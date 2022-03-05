using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages.Manager
{
    public class DeleteCarModel : PageModel
    {
        private readonly ICarService _carService;

        private readonly ILogService _logService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _contextAccessor;

        public DeleteCarModel(ICarService carService, ILogService logService, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _carService = carService;
            _logService = logService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public void OnGet(int id)
        {
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            _carService.Delete(id);
            _logService.Log(Data.LogType.INFO, $"Smazal vozidlo ID {id}", Convert.ToInt32(userId));
        }
    }
}
