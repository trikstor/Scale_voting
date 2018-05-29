using System.Data.Entity;
using ScaleVoting.Domains;

namespace ScaleVoting.Infrastucture
{
    public class PollDbContext : DbContext
    {
        public PollDbContext() : base("name=PollDatabase") { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}