using FleetManager.Data;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FleetManager.Pages
{
    public class ReserveCarModel : PageModel
    {
        private readonly ICarReservationService _carReservationService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReserveCarModel(ICarReservationService carReservationService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _carReservationService = carReservationService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();


        public class InputModel
        {
            public int CarId { get; set; }

            public DateTime From { get; set; }

            public DateTime To { get; set; }

        }
        public void OnGet(int id)
        {
            Input.CarId = id;
        }
        public void OnPost()
        {
            var reservations = _carReservationService.Get(Input.CarId, Input.From);

            if (reservations.Any(a => a.From.DayOfYear == Input.From.DayOfYear && a.From.Year == Input.From.Year
            && a.To.DayOfYear == Input.To.DayOfYear && a.To.Year == Input.To.Year && ( a.From.Hour <= Input.From.Hour || a.To.Hour <= Input.To.Hour))) return;

            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _carReservationService.Create(new Data.CarReservation
            {
                UserId = Convert.ToInt32(userId),
                CarId = Input.CarId,
                From = Input.From,
                To = Input.To
            });
        }
    }
}
