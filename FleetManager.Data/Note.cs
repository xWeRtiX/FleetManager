using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Data
{
    public class Note
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Car Car { get; set; }
    }
}
