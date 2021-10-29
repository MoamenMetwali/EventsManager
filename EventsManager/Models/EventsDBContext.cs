using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Models
{
    public class EventsDBContext : DbContext, IEventsDBContext
    {
        public EventsDBContext(DbContextOptions<EventsDBContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDBContext).Assembly);
        }
    }
}
