using Microsoft.EntityFrameworkCore;
using ThoughtCabinet.Models;

namespace ThoughtCabinet.Data
{
    public class ThoughtsContext : DbContext
    {
        public DbSet<Thought> Thoughts { get; set; }

        public ThoughtsContext(DbContextOptions<ThoughtsContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=EHSSANJPC;Database=ThoughtCabinet;Trusted_Connection=True;");
            }
        }
    }
}
