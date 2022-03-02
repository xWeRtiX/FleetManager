using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Data
{
    public class CarReservation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CarId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public AppUser User { get; set; }

        public Car Car { get; set; }
    }
}
