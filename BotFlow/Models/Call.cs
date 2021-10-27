using System;

namespace BotFlow.Models
{
    public class Call
    {
        public Guid Id { get; set; }
        public Guid IncidentId { get; set; }

        public string Description { get; set; }
        public Incident Incident { get; set; }
        public string Status { get; set; }

    }

}
