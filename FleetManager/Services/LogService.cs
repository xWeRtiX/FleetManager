using FleetManager.Data;
using FleetManager.Services.Interfaces;

namespace FleetManager.Services
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetUserLogs(int id, int count)
        {
            return _context.Logs.Where(w => w.UserId == id).Take(count).OrderByDescending(o => o.Date).ToList();
        }

        public void Log(LogType type, string message, int userId)
        {
            _context.Logs.Add(new Log
            {
                UserId = userId,
                Type = type,
                Text = message,
                Date = DateTime.Now,
            });
            _context.SaveChanges();
        }
    }
}
