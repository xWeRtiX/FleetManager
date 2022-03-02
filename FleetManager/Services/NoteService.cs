using FleetManager.Data;
using FleetManager.Services.Interfaces;

namespace FleetManager.Services
{
    public class NoteService : INoteService
    {
        private readonly AppDbContext _context;

        public NoteService(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Note Note)
        {
            _context.Notes.Add(Note);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var note = _context.Notes.FirstOrDefault(f => f.Id == id);
            if(note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
        }

        public List<Note> GetAll(int carId)
        {
            return _context.Notes.Where(w => w.CarId == carId).ToList();
        }
    }
}
