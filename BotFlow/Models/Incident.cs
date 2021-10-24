using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Models
{
    public class Incident
    {
        public Guid Id { get; set; }
        public string Department { get; set; }
        public string Type { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string CallBackNo { get; set; }

    }
}
