using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotFlow.Models
{
    public class StageConfig
    {
        public Guid Id { get; set; }
        public Stage CurrentStage { get; set; }
        public Guid CurrentStageId { get; set; }
        public Stage NextStage { get; set; }
        public Guid NextStageId { get; set; }

    }
}
