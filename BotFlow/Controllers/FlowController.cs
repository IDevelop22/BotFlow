using BotFlow.Models;
using BotFlow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {
        private readonly EmergencyBotHandler _botHandler;
        private readonly IncidentService _incidentService;
        private readonly ILogger<EmergencyBotHandler> _logger;

        public FlowController(EmergencyBotHandler botHandler, ILogger<EmergencyBotHandler> logger, IncidentService incidentService)
        {
            _botHandler = botHandler;
            _logger = logger;
            _incidentService = incidentService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> IncomingMessage(WebhookObject whObject)
        {
            _logger.LogInformation($"Webhook Payload : {JsonConvert.SerializeObject(whObject)}");
            if (whObject.messages[0].fromMe) return null;
            await _botHandler.HandleIncoming(whObject.messages[0]);
            return JsonConvert.SerializeObject(whObject);
        }

        [HttpGet("incidents")]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents()
        {
            var incidents = await _incidentService.GetIncidents();
            return Ok(incidents);
        }

            [HttpGet("people")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            var people = await _incidentService.GetAvailablePeople();
            return Ok(people);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Incident>> GetIncidentByID(Guid id)
        {
            var incident = await _incidentService.GetIncidentByID(id);
            return Ok(incident);
        }

        [HttpGet("calls")]
        public async Task<ActionResult<IEnumerable<Call>>> GetCalls()
        {
            var calls = await _incidentService.GetCalls();
            return Ok(calls);
        }

        [HttpPost("scheduleCall")]
        [Consumes("application/json")]
        public async Task<object> ScheduleCall(ScheduleCallObject obj)
        {
            var scheduledCall = await _incidentService.ScheduleCall(obj);
            return scheduledCall;
        }




        }
    }
