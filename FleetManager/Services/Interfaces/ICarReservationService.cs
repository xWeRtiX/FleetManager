using FleetManager.Data;

namespace FleetManager.Services.Interfaces
{
    public interface ICarReservationService
    {
        void Create(CarReservation carReservation);

        List<CarReservation> Get(int carId, DateTime date);
        List<CarReservation> GetAll();

    }
}
