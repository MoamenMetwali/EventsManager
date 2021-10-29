using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Models
{
    public interface IEventsDBContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<User> Users { get; set; }
        DatabaseFacade Database { get; }

        int SaveChanges();
    }
}
