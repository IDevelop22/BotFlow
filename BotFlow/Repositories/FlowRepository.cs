using BotFlow.Data;
using BotFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotFlow.Repositories
{
    public class FlowRepository : IFlowRepository
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<FlowRepository> _logger;

        public FlowRepository(PostgresDbContext context, ILogger<FlowRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Call> AddCall(Call call)
        {
            await _context.Calls.AddAsync(call);
            await _context.SaveChangesAsync();
            return call;
        }

        public async Task<Incident> AddIncident(Incident incident)
        {
            using (_context)
            {
                _logger.LogInformation($"Incident to add : {JsonSerializer.Serialize(incident)}");
                await _context.AddAsync(incident);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Added incident {JsonSerializer.Serialize(incident)}");
                return incident;
            }
            
        }

        public async Task<BotFlowInstance> AddInstance(BotFlowInstance instance)
        {
            await _context.Instances.AddAsync(instance);
            await _context.SaveChangesAsync();
            return instance;
        }

        public async Task<PersonCall> AddPersonCall(PersonCall personCall)
        {
            await _context.PersonCalls.AddAsync(personCall);
            await _context.SaveChangesAsync();
            return personCall;
        }

        public async Task<IEnumerable<Stage>> GetAllStages()
        {
            
                var stages = await _context.Stages.ToListAsync();

                return stages;
            
        }

        public  async Task<IEnumerable<Call>> GetCalls()
        {
            var calls = await _context.Calls.ToListAsync();
            return calls;
        }

        public async Task<Incident> GetIncidentByContact(string contact)
        {
            using (_context)
            {
                var incident = await _context.Incidents.Where(i => i.Contact == contact && i.Status == "Logged").FirstOrDefaultAsync();
                return incident;
            }
        }

        public async Task<Incident> GetIncidentById(Guid id)
        {
            var incident = await _context.Incidents.Where(i => i.Id == id).FirstOrDefaultAsync();
            return incident;
        }

        public async Task<IEnumerable<Incident>> GetIncidents()
        {
            using (_context)
            {
                var incidents = await _context.Incidents.ToListAsync();
                return incidents;
            }
        }

        public async Task<BotFlowInstance> GetInstanceByContact(string contact)
        {
            using (_context)
            {
                var instance = await _context.Instances.Where(i => i.Contact == contact && i.Active == true).FirstOrDefaultAsync();
                return instance;
            }
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            var people = await _context.People.ToListAsync();
            return people;
        }

        public async Task<IEnumerable<PersonCall>> GetPeopleCalls()
        {
            var peopleCalls = await _context.PersonCalls.ToListAsync();
            return peopleCalls;
        }

        public async Task<Stage> GetStageById(Guid id)
        {
            using (_context)
            {
                var stage = await _context.Stages.Where(s => s.Id == id).FirstOrDefaultAsync();
                return stage;
            }
        }

        public async Task<StageConfig> GetStageConfig(Guid curretStageId)
        {
            var stageConfig = await _context.StageConfig.Where(s => s.CurrentStageId == curretStageId).FirstOrDefaultAsync();
            return stageConfig;
        }

        public async Task<bool> UpdateIncident(Incident incident)
        {
            using (_context)
            {
                _context.Entry(incident).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateInstance(BotFlowInstance instance)
        {
           
            _context.Entry(instance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
