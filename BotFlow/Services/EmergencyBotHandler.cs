using BotFlow.Models;
using BotFlow.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Services
{
    public class EmergencyBotHandler
    {
        private readonly IFlowRepository _repo;
        private readonly WhatsappMessageService _whatsappMessageService;
        private readonly ILogger<EmergencyBotHandler> _logger;

        public EmergencyBotHandler(ILogger<EmergencyBotHandler> logger, WhatsappMessageService whatsappMessageService, IFlowRepository repo)
        {
            _logger = logger;
            _whatsappMessageService = whatsappMessageService;
            _repo = repo;
        }

        public async Task<bool> HandleIncoming(Message message)
        {
            
            var userResponse = message.body;
            var contact = message.chatId;
            var currentInstance = await _repo.GetInstanceByContact(contact);
            var stages = await _repo.GetAllStages() ;
            if (currentInstance == null)

            {
                var newInstance = new BotFlowInstance()
                {
                    Id= new Guid(),
                    Contact = contact,
                    Active = true,
                    CurrentStage = stages.Where(s=>s.Name=="Welcome" ).FirstOrDefault()
                };

                
                currentInstance = await _repo.AddInstance(newInstance);
            }
            var currentStage = await _repo.GetStageById(currentInstance.CurrentStage.Id);

            var incident = await _repo.GetIncidentByContact(contact);
            if (incident == null)
            {
                var newIncident = new Incident()
                {
                    Id = Guid.NewGuid(),
                    Contact = contact,
                    Status = "Open"
                };

                incident = await _repo.AddIncident(newIncident);
            }

            await HandleNext(currentStage,incident,currentInstance, userResponse);
            return true;

        }

        private async Task<bool> HandleNext(Stage currentStage, Incident incident,BotFlowInstance instance,string response)
        {
            var nextStage = await _repo.GetStageConfig(currentStage.Id);
            switch (currentStage.Name)
            {
                case "Welcome":
                    instance.CurrentStage = nextStage.NextStage;
                    await _repo.UpdateInstance(instance);
                    await _repo.UpdateIncident(incident);
                    await _whatsappMessageService.SendMessage(incident.Contact, currentStage.DisplayMessage);
                    break;
                case "GetContact":
                    instance.CurrentStage = nextStage.NextStage;
                    await _repo.UpdateInstance(instance);
                    incident.CallBackNo = response;
                    await _repo.UpdateIncident(incident);
                    await _whatsappMessageService.SendMessage(incident.Contact, currentStage.DisplayMessage);
                    break;
                case "GetIncident":
                    instance.CurrentStage = nextStage.NextStage;
                    await _repo.UpdateInstance(instance);
                    incident.Type = response;
                    await _repo.UpdateIncident(incident);
                    await _whatsappMessageService.SendMessage(incident.Contact, currentStage.DisplayMessage);
                    break;
                case "GetLocation":
                    instance.CurrentStage = nextStage.NextStage;
                    await _repo.UpdateInstance(instance);
                    incident.Location = response;
                    await _repo.UpdateIncident(incident);
                    await _whatsappMessageService.SendMessage(incident.Contact, currentStage.DisplayMessage);
                    break;
                case "Final":
                    instance.CurrentStage = nextStage.NextStage;
                    await _repo.UpdateInstance(instance);
                    incident.Status = "Closed";
                    await _repo.UpdateIncident(incident);
                    await _whatsappMessageService.SendMessage(incident.Contact, currentStage.DisplayMessage);
                    break;
            }
            return true;
        }

 
    }
}
