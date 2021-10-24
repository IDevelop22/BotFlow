using BotFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Repositories
{
    public interface IFlowRepository
    {
        Task<IEnumerable<Stage>> GetAllStages();
        Task<Stage> GetStageById(Guid id);
        Task<StageConfig> GetStageConfig(Guid curretStageId);
        Task<BotFlowInstance> GetInstanceByContact(string contact);
        Task<IEnumerable<Incident>> GetIncidents();
        Task<Incident> GetIncidentByContact(string contact);

        Task<Incident> AddIncident(Incident incident);
        Task<BotFlowInstance> AddInstance(BotFlowInstance instance);
        Task<bool> UpdateInstance(BotFlowInstance instance);
        Task<bool> UpdateIncident(Incident incident);


    }
}
