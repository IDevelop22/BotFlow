using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Models
{
    public class BotFlowInstance
    {
        public Guid Id { get; set; }
        public string Contact { get; set; }
        public  Stage CurrentStage { get; set; }
        public bool Active { get; set; }
    }
}
