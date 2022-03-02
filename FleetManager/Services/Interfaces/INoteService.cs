using FleetManager.Data;

namespace FleetManager.Services.Interfaces
{
    public interface INoteService
    {
        void Create(Note Note);

        void Delete(int id);

        List<Note> GetAll(int carId);
    }
}
