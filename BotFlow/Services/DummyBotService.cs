using BotFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Services
{
    public class DummyBotService
    {

        public void HandleRequest()
        {
       

        }



         void execute(Stage current,Stage next,Func<bool> handleSpecificStage)
        {
            
        }



        public class Payload
        {
            public string Contact { get; set; }
            public string Response { get; set; }
        }
    }
}
