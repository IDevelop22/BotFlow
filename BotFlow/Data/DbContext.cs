using BotFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace BotFlow.Data
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {

        }

        public DbSet<Stage> Stages { get; set; }
        public DbSet<StageConfig> StageConfig { get; set; }
        public DbSet<BotFlowInstance> Instances { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        public override void Dispose()
        {
            // Dispose of unmanaged resources.
            
        }
    }
}
