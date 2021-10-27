using System;

namespace BotFlow.Models
{
    public class PersonCall
    {
        public Guid Id { get; set; }
        public Guid CallId { get; set; }
        public Guid PersonID { get; set; }

        public string CallStatus { get; set; }
    }

}
