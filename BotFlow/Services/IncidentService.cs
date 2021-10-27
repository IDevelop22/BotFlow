using BotFlow.Models;
using BotFlow.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotFlow.Services
{
    public class IncidentService
    {
        private readonly IFlowRepository _repo;
        private readonly ILogger<IncidentService> _logger;
        private readonly WhatsappMessageService _wms;

        public IncidentService(IFlowRepository repo, ILogger<IncidentService> logger, WhatsappMessageService wms)
        {
            _repo = repo;
            _logger = logger;
            _wms = wms;
        }

        public async Task<IEnumerable<Incident>> GetIncidents()
        {
            var incidents = await _repo.GetIncidents();
            return incidents;
        }

        public async Task<IEnumerable<Person>> GetAvailablePeople()
        {
            var allPeople = await _repo.GetPeopleCalls();
            var onCallPeople = allPeople.Where(pc => pc.CallStatus == "Dispatched");
            _logger.LogWarning("onCall People : " + JsonSerializer.Serialize(onCallPeople));
            List<Person> availablePeople = new List<Person>();
            foreach (var p in await _repo.GetPeople())
            {
                if (p.IsOnline && !(onCallPeople.Where(oc => oc.PersonID == p.Id).Any()))
                {
                    availablePeople.Add(p);
                }
            }

            return availablePeople;
        }

        public async Task<Incident> GetIncidentByID(Guid id)
        {
            var incidents = (await _repo.GetIncidents());
            var incident =  incidents.Where(i => i.Status == "Logged" && i.Id == id).FirstOrDefault();
            return incident;
        }
        public async Task<IEnumerable<Call>> GetCalls()
        {

            var calls = await _repo.GetCalls();
            return calls;
        }

        public async Task<string> ScheduleCall(ScheduleCallObject obj)
        {
            var call = new Call
            {
                IncidentId = obj.IncidentId,
                Status = "Dispatched",
                Description = obj.Description
            };
            await _repo.AddCall(call);
            

            var incident = await _repo.GetIncidentById(obj.IncidentId);
            _logger.LogWarning("callObj : " + JsonSerializer.Serialize(obj));
            foreach (var p in obj.People)
            {
                PersonCall pc = new PersonCall();
                pc.PersonID = p.Id;
                pc.CallId = call.Id;
                pc.CallStatus = "Dispatched";
                await _repo.AddPersonCall(pc);
                await _wms.SendMessage(p.Mobile, "You have been assigned and Emergency call!!you will recieve the directions shortly");
                await _wms.SendLocation(p.Mobile + "@c.us", incident.Location);
            }
            incident.Status = "Dispatched";
            await _repo.UpdateIncident(incident);

            return "Created";
        }

    }
}
