using FleetManager.Data;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Services.Interfaces
{
    public interface ICarService
    {
        void Create(Car car);

        List<Car> GetCars();

        void Delete(int id);

        Car GetCar(int id);

        void Edit(Car car);
    }
}
