using FleetManager.Data;
using FleetManager.Services.Interfaces;

namespace FleetManager.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _context.Cars.FirstOrDefault(f => f.Id == id);
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }

        public void Edit(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.FirstOrDefault(f => f.Id == id);

        }

        public List<Car> GetCars()
        {
            return _context.Cars.ToList();
        }


    }
}
