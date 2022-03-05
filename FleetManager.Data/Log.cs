using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Data
{
    public class Log
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public LogType Type { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public AppUser User { get; set; }
    }

    public enum LogType
    {
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }
}
