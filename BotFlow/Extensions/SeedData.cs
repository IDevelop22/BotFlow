using BotFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotFlow.Extensions
{
    public static partial class HostBuilderExtension
    {
        public static class SeedData
        {
            public static IEnumerable<Stage> GetStages() { 
                return new List<Stage>()
            {
                new Stage{ Id = Guid.NewGuid(),Name = "Welcome",DisplayMessage = $"Welcome,What would you like to do {Environment.NewLine}1. Fire and Emergency {Environment.NewLine}2. SAPS ",IsInitital = true },
                new Stage{ Id = Guid.NewGuid(),Name = "GetContact",DisplayMessage = "Please provide a number where we can call you back in the format (0831234567) or \"0\" to go back : ",IsInitital = false },
                new Stage{ Id = Guid.NewGuid(),Name = "GetIncidentType",DisplayMessage = "Please provide the type of incident(1-Accident,2-Building Fire,3-Wild Fire):  ",IsInitital = false },
                new Stage{ Id = Guid.NewGuid(),Name = "GetLocation",DisplayMessage = "Please provide the location of the Incident(click on the money clip icon and select location and send): ",IsInitital = false },
                new Stage{ Id = Guid.NewGuid(),Name = "Final",DisplayMessage = "Thank you!Your call has been logged,you will recieve a call from one of our dispatchers,press 0 to return to main menu: ): ",IsInitital = false }

            };
            }

            public static IEnumerable<StageConfig> GetStageConfig(IEnumerable<Stage> stages)
            {
                return new List<StageConfig>()
                {
                    new StageConfig(){  Id = Guid.NewGuid(),CurrentStageId = stages.Where(s=>s.Name=="Welcome").FirstOrDefault().Id,NextStageId = stages.Where(s=>s.Name=="GetContact").FirstOrDefault().Id},
                    new StageConfig(){  Id = Guid.NewGuid(),CurrentStageId = stages.Where(s=>s.Name=="GetContact").FirstOrDefault().Id,NextStageId = stages.Where(s=>s.Name=="GetIncidentType").FirstOrDefault().Id},
                    new StageConfig(){  Id = Guid.NewGuid(),CurrentStageId = stages.Where(s=>s.Name=="GetIncidentType").FirstOrDefault().Id,NextStageId = stages.Where(s=>s.Name=="GetLocation").FirstOrDefault().Id},
                    new StageConfig(){  Id = Guid.NewGuid(),CurrentStageId = stages.Where(s=>s.Name=="GetLocation").FirstOrDefault().Id,NextStageId = stages.Where(s=>s.Name=="Final").FirstOrDefault().Id},
                    new StageConfig(){  Id = Guid.NewGuid(),CurrentStageId = stages.Where(s=>s.Name=="Final").FirstOrDefault().Id,NextStageId = stages.Where(s=>s.Name=="Welcome").FirstOrDefault().Id}
                };
            }
        }

    }
}
