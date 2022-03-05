using FleetManager.Data;
using FleetManager.Services.Interfaces;

namespace FleetManager.Services
{
    public class CarReservationService : ICarReservationService
    {
        private readonly AppDbContext _context;

        public CarReservationService(AppDbContext context)
        {
            _context = context;
        }

        public void Create(CarReservation carReservation)
        {
            _context.CarReservations.Add(carReservation);
            _context.SaveChanges();
        }

        public List<CarReservation> Get(int carId, DateTime date)
        {
            return _context.CarReservations.Where(w => w.CarId == carId && w.From.DayOfYear == date.DayOfYear && w.From.Year == date.Year).ToList();
        }

        public List<CarReservation> GetAll()
        {
            return _context.CarReservations.ToList();
        }

        /*
public List<CarReservation> GetAll()
{
   return true;
}*/
    }
}
