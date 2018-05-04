using System.Data.Entity;
using ScaleVoting.Domains;

namespace ScaleVoting.Infrastucture
{
    public class PollDbContext : DbContext
    {
        public PollDbContext() : base("name=PollDatabase")
        {
        }

        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>()
                .HasMany(s => s.Questions)
                .WithRequired(s => s.ParentPoll)
                .HasForeignKey(s => s.ParentPollId);

            modelBuilder.Entity<Question>()
                .Ignore(s => s.TotalAnswer)
                .HasMany(s => s.Options)
                .WithRequired(s => s.ParentQuestion)
                .HasForeignKey(s => s.ParentQuestionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}