using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarReservation> CarReservations { get; set; }

        public DbSet<Note> Notes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }
    }
}
