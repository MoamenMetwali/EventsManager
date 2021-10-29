﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Models
{
    public class User
    {
        public User()
        {
            Events = new HashSet<Event>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public virtual ICollection<Event> Events { get; private set; }
    }
}
