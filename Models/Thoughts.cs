using Microsoft.EntityFrameworkCore;

namespace ThoughtCabinet.Models
{
    public class Thoughts
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ThoughtsContext : DbContext
    {
        public DbSet<Thoughts> Thoughts { get; set; }

        public ThoughtsContext(DbContextOptions<ThoughtsContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=EHSSANJPC;Database=ThoughtCabinet;Trusted_Connection=True;");
            }
        }
    }
}
