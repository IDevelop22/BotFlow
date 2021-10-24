using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Models
{
    public class Stage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsInitital { get; set; }
        public string DisplayMessage { get; set; }
    }
}
