using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Models
{
    public class Message
    {
        public string id { get; set; }
        public string body { get; set; }
        public string type { get; set; }
        public string senderName { get; set; }
        public bool fromMe { get; set; }
        public string author { get; set; }
        public int time { get; set; }
        public string chatId { get; set; }
        public int messageNumber { get; set; }
    }
    public class WebhookObject
    {
        public string instanceId { get; set; }
        public List<Message> messages { get; set; }
    }
}
