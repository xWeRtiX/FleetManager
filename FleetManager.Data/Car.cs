using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Data
{
    public class Car
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime AssembledAt { get; set; }

        public DateTime TechnicalInspectionDate { get; set; }

        public string VIN { get; set; }

        public string Identificator { get; set; }

        public List<CarReservation> CarReservations { get; set; }
    }
}
