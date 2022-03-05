using FleetManager.Data;

namespace FleetManager.Services.Interfaces
{
    public interface ILogService
    {
        void Log(LogType type, string message, int userId);

        List<Log> GetUserLogs(int id, int count);
    }
}
