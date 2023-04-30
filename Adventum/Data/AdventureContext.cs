using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Adventum.Data
{
    public class AdventureContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<SportActivity> SportActivities { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<EventReservation> EventReservations { get; set; }
        public AdventureContext(DbContextOptions<AdventureContext> options)
            : base(options)
        {
        }
    }
}