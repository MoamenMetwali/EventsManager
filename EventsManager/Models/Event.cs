using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsManager.Models
{
    public class Event
    {
        public Event()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UsersNo { get; set; }
        public virtual ICollection<User> Users { get; private set; }
    }
}
