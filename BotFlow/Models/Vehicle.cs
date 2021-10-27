using System;

namespace BotFlow.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string VehReg { get; set; }
        public bool IsOnline { get; set; }
    }

}
