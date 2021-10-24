using BotFlow.Models;
using BotFlow.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {
        private readonly EmergencyBotHandler _botHandler;

        public FlowController(EmergencyBotHandler botHandler)
        {
            _botHandler = botHandler;
        }

        [HttpPost]
        public async Task<ActionResult<string>> IncomingMessage(WebhookObject whObject)
        {
            if (whObject.messages[0].fromMe) return null;
            await _botHandler.HandleIncoming(whObject.messages[0]);
            return JsonSerializer.Serialize(whObject);
        }
    }
}
