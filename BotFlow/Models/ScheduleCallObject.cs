using System;
using System.Collections.Generic;

namespace BotFlow.Models
{
    public class ScheduleCallObject
    {
        public Guid IncidentId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public ICollection<Person> People { get; set; }
    }

}
